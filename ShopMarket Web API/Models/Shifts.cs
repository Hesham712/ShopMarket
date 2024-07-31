using ShopMarket_Web_API.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShopMarket_Web_API.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime StartShift { get; set; } = DateTime.Now;
        public DateTime? EndShift { get; set; }
        public Decimal TotalCash { get; set; }  = Decimal.Zero;
        public StatusShift Status { get; set; } = StatusShift.Active;
        public int UserId { get; set; }
        public User? User { get; set; }
        public IList<Order>? Orders { get; set; }

    }
}
