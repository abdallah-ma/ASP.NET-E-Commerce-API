namespace DemoAPI.Common.Errors
{
    public class BadRequestResponse : ApiResponse
    {
        
        
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public BadRequestResponse() : base(400)
        {

        }
        

    }



}
