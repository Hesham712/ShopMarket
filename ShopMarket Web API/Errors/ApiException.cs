namespace ShopMarket_Web_API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public int StatusCode { get; set; }
        public String Message { get; set; }
    }
}
