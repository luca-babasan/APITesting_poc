using Newtonsoft.Json;

namespace APITesting.Models
{
    public class CreateUserRequest
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
       
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        public string Job { get; set; }
    }
}
