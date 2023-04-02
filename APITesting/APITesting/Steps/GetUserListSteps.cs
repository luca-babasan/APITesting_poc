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
    public class GetUserListSteps
    {
        private HttpResponseMessage _httpResponse;
        private readonly SettingsProvider settingsProvider;
        private string _jsonResponse;

        public GetUserListSteps(SettingsProvider settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }
        [Given(@"I populate the API call to get a list of user")]
        public async Task GivenIPopulateTheAPICallToGetAListOfUser()
        {
            string ENDPOINT = settingsProvider.ApiUsersEndpoint.ToString();
            var httpClient = new HttpClient();
            //var serialized = JsonConvert.SerializeObject(createUserRequest);
            //var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            _httpResponse = await httpClient.GetAsync(ENDPOINT + "?per_page=100");
            _httpResponse.EnsureSuccessStatusCode(); 
        }

        [When(@"I receive the API call response with the list")]
        public async Task<string> WhenIReceiveTheAPICallResponseWithTheListAsync()
        {
            if (_httpResponse.IsSuccessStatusCode)
            {
                _jsonResponse = await _httpResponse.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(_httpResponse.ToString());
            }
            return _jsonResponse;
        }

        [Then(@"the list should contain info on all users")]
        public Task ThenTheListShouldContainInfoOnAllUsersAsync()
        {
            Console.WriteLine(_jsonResponse);
            return Task.CompletedTask;
        }
    }
}
