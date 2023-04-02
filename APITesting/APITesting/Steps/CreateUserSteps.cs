using APITesting.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using APITesting.Services;
using NUnit.Framework;

namespace APITesting.Steps
{
    [Binding]
    public class CreateUserSteps
    {
        
        private HttpResponseMessage _httpResponse;
        private readonly SettingsProvider settingsProvider;
        private CreateUserRequest createUserRequest;
        private CreateUserResponse createUserResponse;
        private string jsonResponse;

        public CreateUserSteps(SettingsProvider settingsProvider, CreateUserRequest createUserRequest, CreateUserResponse createUserResponse)
        {
            this.settingsProvider = settingsProvider;
            this.createUserRequest = createUserRequest;
            this.createUserResponse = createUserResponse;
        }

        [Given(@"I populate the API call")]
        public void GivenIPopulateTheAPICall()
        {
            createUserRequest = new CreateUserRequest
            {
                Email = settingsProvider.TestUserEmail.ToString(),
                FirstName = settingsProvider.TestUserFirstName.ToString(),
                LastName = settingsProvider.TestUserLastName.ToString(),
                Job = settingsProvider.TestUserJob.ToString()
            };
        }

        [When(@"I make the API call to create a new user")]
        public async Task WhenIMakeTheAPICallToCreateANewUser()
        {
            var ENDPOINT = settingsProvider.ApiUsersEndpoint;
            var httpClient = new HttpClient();
            var serialized = JsonConvert.SerializeObject(createUserRequest);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            _httpResponse = await httpClient.PostAsync(ENDPOINT, stringContent);
            //_httpResponse.EnsureSuccessStatusCode();
            if (_httpResponse.IsSuccessStatusCode)
            {
                jsonResponse = await _httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse); 
            }
            else
            {
                throw new Exception(_httpResponse.ToString());
            }

        }

        [Then(@"the call is successful")]
        public void ThenTheCallIsSuccessful()
        {
            var code = (int)_httpResponse.StatusCode;
            Console.WriteLine("Response is: "+_httpResponse.StatusCode+" with status code: " +code);
            
            Assert.IsTrue(_httpResponse.IsSuccessStatusCode);
        }

        [Then(@"the user profile is created")]
        public void ThenTheUserProfileIsCreated()
        {
            Console.WriteLine(jsonResponse.ToString());
            jsonResponse.CompareTo(createUserResponse.ToString());
        }
    }
}
