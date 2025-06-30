namespace DemoAPI.Common.Errors
{
    public class ApiResponse
    {

        public int Code { get; set; }

        public string Message { get; set; }

        
        public ApiResponse(int code,string message)
        {
            Code = code;
            Message = message;
        }

        public ApiResponse(int code)
        {
            Code = code;
            
   
                Message = code switch
                {
                    400 => "Bad Request",
                    401 => "Unauthorized",
                    404 => "Resource not found",
                    500 => "Internal server error",
                    _ => null
                };

            
        }





    }
}
