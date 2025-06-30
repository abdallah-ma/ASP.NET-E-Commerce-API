namespace DemoAPI.Common.Errors
{
    public class InternalErrorResponse : ApiResponse
    {

        public string? Details { get; set; } = null;

        public InternalErrorResponse() : base(500)
        {
        }

        public InternalErrorResponse(int code,string message,string? details) : base(500,message)
        {
            Details = details;
        }
        


    }
}
