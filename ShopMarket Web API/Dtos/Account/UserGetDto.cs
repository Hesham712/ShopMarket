namespace ShopMarket_Web_API.Dtos.Account
{
    public class UserGetDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailConfirmed { get; set; }
        
    }
}
