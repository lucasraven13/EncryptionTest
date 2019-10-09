using Newtonsoft.Json;

namespace APIGateway.Models
{
    public class ApiErrorResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}