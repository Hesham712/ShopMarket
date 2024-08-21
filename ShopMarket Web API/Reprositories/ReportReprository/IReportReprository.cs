using ShopMarket_Web_API.Dtos.Report;

namespace ShopMarket_Web_API.Reprositories.ReportReprository
{
    public interface IReportReprository
    {
        Task<List<ReportDayDto>> CreateReportMonthly(DateTime reportDate);
        Task<List<ReportDayDetailsDto>> CreateReportMonthlyDetails(DateTime reportDate);
        Task<List<ReportMonthDto>> CreateReportAnnual(int Year);
    }
}
