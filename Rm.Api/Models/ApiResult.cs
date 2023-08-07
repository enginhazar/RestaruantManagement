namespace Rm.Api.Models
{
    public class ApiResult<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string ErrorMessage { get; set; }
    }
}
