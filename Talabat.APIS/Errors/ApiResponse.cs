
namespace Talabat.APIS.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statuscode, string? message = null)
        {
            StatusCode = statuscode;
            Message = Message ?? GetDefaultMessageForStatusCode(statuscode);
        }

        private string? GetDefaultMessageForStatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => "Bad Request",
                401 => "UnAthorized",
                404 => "Not Found",
                500 => "Server Error",
                _ => null

            };
        }
    }
}
