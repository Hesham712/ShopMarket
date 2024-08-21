using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMarket_Web_API.Reprositories.ReportReprository;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportReprository _reportReprository;

        public ReportsController(IReportReprository reportReprository)
        {
            _reportReprository = reportReprository;
        }

        [HttpGet("ReportMonthly")]
        public async Task<IActionResult> GetReportMonthly([FromQuery][Required] DateTime ReprotDate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());

            var result = await _reportReprository.CreateReportMonthly(ReprotDate);
            return Ok(result);
        }

        [HttpGet("ReportMonthlyDetails")]
        public async Task<IActionResult> GetReportMonthlyDetails([FromQuery][Required] DateTime ReprotDate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());

            var result = await _reportReprository.CreateReportMonthlyDetails(ReprotDate);
            return Ok(result);
        }

        [HttpGet("ReportAnnual")]
        public async Task<IActionResult> GetReportAnnual([FromQuery][Required] int Year)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());

            
            var result = await _reportReprository.CreateReportAnnual(Year);
            return Ok(result);
        }
    }
}
