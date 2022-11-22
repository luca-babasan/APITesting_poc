using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace APITesting.Services
{
    public class SettingsProvider
    {
        private List<EnvironmentSetting> settings;
        // Get variables from environment
        //we can switch between environments by replacing "DEV" with "QA" or "PROD" if we have configs in Settings
        public static string Environment => System.Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "DEV";

        public static string OutputPath => System.Environment.GetEnvironmentVariable("OUTPUT_PATH") ??
                                    Path.Combine(System.Environment.CurrentDirectory, "output");

        //--------------------------------------
        //The values from the Environment.DEV.Json are loaded here so we can use them in our steps
        public string BaseUrl => GetSettings("API_URL");
        public string ApiUsersEndpoint => GetSettings("API_USERS");
        public string TestUserEmail => GetSettings("TEST_USER_EMAIL");
        public string TestUserFirstName => GetSettings("TEST_USER_FIRST_NAME");
        public string TestUserLastName => GetSettings("TEST_USER_LAST_NAME");
        public string TestUserJob => GetSettings("TEST_USER_JOB");


        //--------------------------------------
        public SettingsProvider()
        {
            LoadSettings();
        }

        private string GetSettings(string key)
        {
            return System.Environment.GetEnvironmentVariable(key)
                   ?? settings.FirstOrDefault(x => x.Key == key)?.Value
                   ?? throw new InvalidOperationException($"key {key} not found");
        }

        private void LoadSettings()
        {
            var baseDir = Directory.GetParent(System.Environment.CurrentDirectory).Parent?.Parent?.FullName;
            if (string.IsNullOrWhiteSpace(baseDir))
            {
                throw new Exception("Invalid Base Dir");
            }

            var environment = Environment;

            var filePath = Path.Combine(baseDir, $"Settings/Environment.{environment}.Json");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                settings = JsonSerializer.Deserialize<List<EnvironmentSetting>>(json);
            }
            else
            {
                settings = new List<EnvironmentSetting>();
            }
        }
       
    }
}