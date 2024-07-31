using Microsoft.AspNetCore.Identity;

namespace ShopMarket_Web_API.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public List<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
