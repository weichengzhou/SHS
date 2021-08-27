namespace SHS.Models
{
    public interface IResponse
    {
    }    

    public class ShsResponse : IResponse
    {
        public string ResponseCode { get; set; }

        public string Message { get; set; }

        public object Results { get; set; }
    }

    public class ShsFieldError
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}