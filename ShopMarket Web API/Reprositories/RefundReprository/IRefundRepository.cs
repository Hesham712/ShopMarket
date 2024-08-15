using ShopMarket_Web_API.Dtos.Refund;
using ShopMarket_Web_API.Dtos.Product;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Reprository.RefundReprository
{
    public interface IRefundRepository
    {
        Task<List<RefundItemsDetailDto>> CreateRefundAsync(List<RefundItemsRequestDto> RefundItemsDto, int ShiftId,int OrderId);
        Task<IList<RefundDto>> GetRefundAsync();
        Task<RefundDetailsDto> GetRefundByIdAsync(int refundId);

    }
}
