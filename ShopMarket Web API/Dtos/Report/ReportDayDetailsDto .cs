using ShopMarket_Web_API.Dtos.Shift;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Dtos.Report
{
    public class ReportDayDetailsDto
    {
        public int Day { get; set; }
        public decimal TotalCash { get; set; }
        public List<ShiftDetailsReport> Shifts { get; set; }

    }
}
