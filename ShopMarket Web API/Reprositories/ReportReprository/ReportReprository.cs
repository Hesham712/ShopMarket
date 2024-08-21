using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using ShopMarket_Web_API.Data;
using ShopMarket_Web_API.Dtos.Report;
using ShopMarket_Web_API.Dtos.Shift;
using System.Globalization;

namespace ShopMarket_Web_API.Reprositories.ReportReprository
{
    public class ReportReprository : IReportReprository
    {
        private readonly ApplicationDbContext _context;

        public ReportReprository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReportDayDto>> CreateReportMonthly(DateTime ReprotDate)
        {
            var ReportMonthly = await _context.Shifts.Where(m => m.StartShift.Month == ReprotDate.Month && m.StartShift.Year == ReprotDate.Year)
                .GroupBy(m => m.StartShift.Day)
                .Select(m => new ReportDayDto
                {
                    Day = m.Key,
                    TotalCash = m.Sum(m => m.TotalCash)
                }).ToListAsync();

            if (ReportMonthly.IsNullOrEmpty())
                throw new ArgumentException($"there no data in Date : {ReprotDate.ToString("MM,yyyy")}");
            return ReportMonthly;
        }
        public async Task<List<ReportDayDetailsDto>> CreateReportMonthlyDetails(DateTime ReprotDate)
        {
            var ReportMonthly = await _context.Shifts.Where(m => m.StartShift.Month == ReprotDate.Month
                                                            && m.StartShift.Year == ReprotDate.Year)
                .Include(m => m.User)
                .GroupBy(m => m.StartShift.Day)
                .Select(s => new ReportDayDetailsDto
                {
                    Day = s.Key,
                    TotalCash = s.Sum(m => m.TotalCash),
                    Shifts = new List<ShiftDetailsReport>
                    {
                        new ShiftDetailsReport {
                            ShiftId = s.Key,
                            UserName = s.First().User.UserName,
                            TotalCash = s.Sum(m=>m.TotalCash)
                        }
                    }
                }).ToListAsync();

            if (ReportMonthly.IsNullOrEmpty())
                throw new ArgumentException($"there no data in Date : {ReprotDate.ToString("MM,yyyy")}");

            return ReportMonthly;
        }
        public async Task<List<ReportMonthDto>> CreateReportAnnual(int Year)
        {
            if (Year < 2000 || Year > DateTime.Now.Year)
                throw new ArgumentException("Please enter a valid year.");

            var reportAnnual = await _context.Shifts.Where(m => m.StartShift.Year == Year)
                .GroupBy(m => m.StartShift.Month)
                .Select(m => new ReportMonthDto
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m.Key),
                    TotalCash = m.Sum(m => m.TotalCash)
                }).ToListAsync();

            if (reportAnnual.IsNullOrEmpty())
                throw new ArgumentException($"there no data in year : {Year}");

            return reportAnnual;
        }

    }
}
