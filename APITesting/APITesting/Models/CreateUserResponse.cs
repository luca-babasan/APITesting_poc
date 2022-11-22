using Newtonsoft.Json;

namespace APITesting.Models
{
    public class CreateUserResponse
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Job { get; set; }

        [JsonProperty(PropertyName = "job")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public string CreatedAt { get; set; }
    }
}
