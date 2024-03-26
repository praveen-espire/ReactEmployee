using Newtonsoft.Json;

namespace Entities.AppEntity
{
    public class ErrorResponse
    {
        [JsonProperty("errormessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("errordetail")]
        public string ErrorDetail { get; set; }
    }
}
