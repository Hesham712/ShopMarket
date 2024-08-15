using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopMarket_Web_API.Data;
using ShopMarket_Web_API.Dtos.Refund;
using ShopMarket_Web_API.Models;
using ShopMarket_Web_API.Reprository.OrderReprository;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Reprository.RefundReprository
{
    public class RefundRepository : IRefundRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public RefundRepository(ApplicationDbContext context, IMapper mapper, IOrderRepository orderRepository)
        {
            _context = context;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<List<RefundItemsDetailDto>> CreateRefundAsync(List<RefundItemsRequestDto> RefundItemsDto,[Required] int ShiftId,[Required] int OrderId)
        {
            using (var Transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (!await _orderRepository.OrderExist(OrderId))
                        throw new ArgumentException(message : $"this order Id : {OrderId} not found");

                    var shift = await _context.Shifts.FirstOrDefaultAsync(m => m.Id == ShiftId && m.EndShift == null);
                    if (shift is null)
                        throw new ArgumentException($"this Shift Id : {ShiftId} not active");

                    var refund = new Refund()
                    {
                        ShiftId = ShiftId,
                        OrderId = OrderId
                    };
                    await _context.AddAsync(refund);
                    await _context.SaveChangesAsync();

                    var refundItem = new List<RefundItem>();

                    foreach (var Refunditem in RefundItemsDto)
                    {
                        var orderItem = await _context.OrderItems.Include(p=>p.Product).FirstOrDefaultAsync(p => p.Id == Refunditem.OrderItemId);
                        
                        if (orderItem is null)
                            throw new InvalidOperationException($"order Item with ID {Refunditem.OrderItemId} not found.");

                        var totalRefundedQuantity = await _context.RefundItems.Where(p => p.OrderItemId == Refunditem.OrderItemId).SumAsync(p => p.Quantity);

                        if (orderItem.Quantity < totalRefundedQuantity + Refunditem.Quantity)
                            throw new InvalidOperationException($"Cannot refund {Refunditem.Quantity} items for Order Item ID {Refunditem.OrderItemId}. " +
                                                                $"Already refunded: {totalRefundedQuantity}, Ordered: {orderItem.Quantity}.");

                        orderItem.Product!.Stock += Refunditem.Quantity;
                        refund.TotalPrice += orderItem.Product.Price * Refunditem.Quantity;

                        refundItem.Add(new RefundItem()
                        {
                            RefundId = refund.Id,
                            Quantity = Refunditem.Quantity,
                            OrderItemId = Refunditem.OrderItemId,
                        });
                    }
                    shift.TotalCash -= refund.TotalPrice;

                    await _context.RefundItems.AddRangeAsync(refundItem);
                    await _context.SaveChangesAsync();

                    await Transaction.CommitAsync();

                    return (_mapper.Map<List<RefundItemsDetailDto>>(refundItem));
                }
                catch (Exception ex)
                {
                    await Transaction.RollbackAsync();
                    throw new InvalidOperationException(message : ex.Message.ToString());
                }
            }
        }

        public async Task<IList<RefundDto>> GetRefundAsync()=>
            _mapper.Map<List<RefundDto>>(await _context.Refunds.ToListAsync());

        public async Task<RefundDetailsDto> GetRefundByIdAsync(int refundId)
        {
            var refund = await _context.Refunds.Include(p => p.RefundItems).FirstOrDefaultAsync(p => p.Id == refundId);

            if (refund is null)
                throw new InvalidOperationException($"Refund with ID {refundId} not found.");

            return (_mapper.Map<RefundDetailsDto>(refund));
        }
    }
}
