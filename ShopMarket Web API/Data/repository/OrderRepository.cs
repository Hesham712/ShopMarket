using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ShopMarket_Web_API.Data.Interface;
using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Data.repository
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
                    var shiftExist = await _context.Shifts.FirstOrDefaultAsync(m => m.Id == shiftId && m.EndShift == null);
                    if (shiftExist == null)
                        throw new ArgumentException("Shift does not exist or has already ended.");
                    //create new order
                    var order = new Order()
                    {
                        ShiftId = shiftId
                    };
                    await _context.AddAsync(order);
                    await _context.SaveChangesAsync();

                    //check if productId not found and calc total price of order
                    foreach (var item in orderItemsDto)
                    {
                        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);

                        if (product == null)
                        {
                            throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");
                        }
                        if (product.Stock < item.Quantity)
                        {
                            throw new InvalidOperationException($"Insufficient stock for product ID {item.ProductId}. Available: {product.Stock}, Requested: {item.Quantity}");
                        }
                    }
                    //update stock product
                    foreach (var item in orderItemsDto)
                    {
                        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                        product.Stock -= item.Quantity;
                    }
                    //calc total order price
                    foreach (var item in orderItemsDto)
                    {
                        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                        order.TotalPrice += product.Price * item.Quantity;
                    }
                    shiftExist.TotalCash += order.TotalPrice;

                    var itemOrder = new List<OrderItem>();

                    orderItemsDto.ForEach(x => itemOrder.Add(new OrderItem()
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        OrderId = order.Id,
                        Price = x.Quantity * _productRepository.GetProductByIdAsync(x.ProductId).Result.Price
                    }));
                    await _context.OrderItems.AddRangeAsync(itemOrder);
                    await _context.SaveChangesAsync();
                    var ListOfItems = _mapper.Map<List<OrderItemsDetailDto>>(itemOrder);
                    return ListOfItems;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public Task<IList<Order>> GetOrderAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderByIdDto> GetOrderByIdAsync(int orderId)
        {
            //var order = await _context.Orders.FindAsync(orderId);
            //if(order == null)
            //    throw new InvalidOperationException($"Order with ID {orderId} not found.");

            ////order = _mapper.Map<OrderByIdDto>(order);

            //return order;
            throw new NotImplementedException();

        }
    }
}
