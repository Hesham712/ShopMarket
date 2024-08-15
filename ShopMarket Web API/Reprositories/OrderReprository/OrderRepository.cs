using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ShopMarket_Web_API.Data;
using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Models;
using ShopMarket_Web_API.Reprositories.ProductReprository;

namespace ShopMarket_Web_API.Reprository.OrderReprository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, IProductRepository productRepository, IMapper mapper)
        {
            _context = context;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderItemsDetailDto>> CreateOrderAsync(List<OrderItemsRequestDto> orderItemsDto, int shiftId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //chech if shift is active
                    var shift = await _context.Shifts.FirstOrDefaultAsync(m => m.Id == shiftId && m.EndShift == null);
                    if (shift is null)
                        throw new ArgumentException("Shift does not exist or has already ended.");
                    //create new order
                    var order = new Order()
                    {
                        ShiftId = shiftId
                    };
                    await _context.AddAsync(order);
                    await _context.SaveChangesAsync();

                    var itemOrder = new List<OrderItem>();

                    //check if productId not found and calc total price of order
                    foreach (var item in orderItemsDto)
                    {
                        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId && p.IsDeleted == false);

                        if (product is null)
                            throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");

                        if (product.Stock < item.Quantity)
                            throw new InvalidOperationException($"Insufficient stock for product ID {item.ProductId}. Available: {product.Stock}, Requested: {item.Quantity}");

                        product.Stock -= item.Quantity;
                        order.TotalPrice += product.Price * item.Quantity;
                        itemOrder.Add(new OrderItem()
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            OrderId = order.Id,
                            Price = item.Quantity * _productRepository.GetProductByIdAsync(item.ProductId).Result.Price
                        });
                    }
                    shift.TotalCash += order.TotalPrice;

                    await _context.OrderItems.AddRangeAsync(itemOrder);
                    await _context.SaveChangesAsync();

                    var ListOfItems = _mapper.Map<List<OrderItemsDetailDto>>(itemOrder);

                    transaction.Commit();
                    return ListOfItems;
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<IList<OrderDto>> GetOrderAsync() =>
            _mapper.Map<List<OrderDto>>(await _context.Orders.ToListAsync());

        public async Task<OrderByIdDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders.Include(p => p.OrderItems).FirstOrDefaultAsync(p => p.Id == orderId);

            if (order is null)
                throw new InvalidOperationException($"Order with ID {orderId} not found.");

            return _mapper.Map<OrderByIdDto>(order);
        }

        public async Task<bool> OrderExist(int orderId) =>
            (await _context.Orders.AnyAsync(m => m.Id == orderId));
    }
}
