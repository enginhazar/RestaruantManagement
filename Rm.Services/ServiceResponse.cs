namespace Rm.Services
{
    public class ServiceResponse<T>
    {
        private ErrorResponse errorResponse;
        public ServiceResponse()
        {
            errorResponse = new ErrorResponse();
        }
        public bool Success { get; set; }
        public T Data { get; set; }

        public ErrorResponse Error { get { return errorResponse; } set { errorResponse = value; } }

    }

    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
    }


}
