using KARE.E2E.AUTOMATION.Models.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KARE.E2E.AUTOMATION.Helpers
{
    public static class ConfigHelper
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(System.AppContext.BaseDirectory).AddJsonFile("EnvironmentSettings.json", optional: true, reloadOnChange: true);
            return builder.Build();            
        }

        public static void ModifyRegisteredEmail(String value)
        {
            GetConfig()["RegisteredEmail"] = value;
        }
        public static string GetEnvironment()
        {
            return GetConfig()["Environment"];
        }
        public static string GetBrowser()
        {
            return GetConfig()["Browser"];
        }
        public static string GetURL()
        {
            return GetConfig()["URL"];
        }
        public static string GetAPIBaseURL()
        {
            return GetConfig()["BaseURL"];
        }
        public static string GetLogSwitch()
        {
            return GetConfig()["LogSwitch"];
        }
        public static string GetLogLanguage()
        {
            return GetConfig()["LogLanguage"];
        }
        public static string GetFailLogLanguage()
        {
            return GetConfig()["FailLogLanguage"];
        }
        public static string GetEmail()
        {
            return GetConfig()["Email"];
        }
        public static string GetPassword()
        {
            return GetConfig()["Password"];
        }
        public static string GetAppVersion()
        {
            return GetConfig()["AppVersion"];
        }

        public static void UpdateJsonData(String emailAddress, String firstName, String lastName, String phoneNumber)
        {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            string filePath = projectPath.ToString() + "Data\\API\\TestData\\Herodata.json";

            // Check if the JSON file exists
            if (File.Exists(filePath))
            {
                // Read the existing JSON data from the file
                string existingJson = File.ReadAllText(filePath);

                // Deserialize the JSON data into a C# object
                HeroData existingData = JsonSerializer.Deserialize<HeroData>(existingJson);

                // Modify the object by adding new values
                existingData.EmailAddress = emailAddress;
                existingData.FirstName = firstName;
                existingData.LastName = lastName;
                existingData.PhoneNumber = phoneNumber;

                // Serialize the updated object back to JSON
                string updatedJson = JsonSerializer.Serialize(existingData);

                // Write the updated JSON back to the file
                File.WriteAllText(filePath, updatedJson);
            }
        }

        public static HeroData ReadHeroData()
        {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            string filePath = projectPath.ToString()+"Data\\API\\TestData\\Herodata.json";
            HeroData data = null;

            if (File.Exists(filePath))
            {               
                string json = File.ReadAllText(filePath);                
                data = JsonSerializer.Deserialize<HeroData>(json);                
            }            

            return data;
        }

        public static void Sidekick1UpdateJsonData(String emailAddress, String firstName, String lastName, String phoneNumber)
        {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            string filePath = projectPath.ToString() + "Data\\API\\TestData\\Sidekick1Herodata.json";

            // Check if the JSON file exists
            if (File.Exists(filePath))
            {
                // Read the existing JSON data from the file
                string existingJson = File.ReadAllText(filePath);

                // Deserialize the JSON data into a C# object
                HeroData existingData = JsonSerializer.Deserialize<HeroData>(existingJson);

                // Modify the object by adding new values
                existingData.EmailAddress = emailAddress;
                existingData.FirstName = firstName;
                existingData.LastName = lastName;
                existingData.PhoneNumber = phoneNumber;

                // Serialize the updated object back to JSON
                string updatedJson = JsonSerializer.Serialize(existingData);

                // Write the updated JSON back to the file
                File.WriteAllText(filePath, updatedJson);
            }
        }

        public static HeroData Sidekick1ReadHeroData()
        {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            string filePath = projectPath.ToString() + "Data\\API\\TestData\\Sidekick1Herodata.json";
            HeroData data = null;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                data = JsonSerializer.Deserialize<HeroData>(json);
            }

            return data;
        }

        public static void Sidekick2UpdateJsonData(String emailAddress, String firstName, String lastName, String phoneNumber)
        {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            string filePath = projectPath.ToString() + "Data\\API\\TestData\\Sidekick2Herodata.json";

            // Check if the JSON file exists
            if (File.Exists(filePath))
            {
                // Read the existing JSON data from the file
                string existingJson = File.ReadAllText(filePath);

                // Deserialize the JSON data into a C# object
                HeroData existingData = JsonSerializer.Deserialize<HeroData>(existingJson);

                // Modify the object by adding new values
                existingData.EmailAddress = emailAddress;
                existingData.FirstName = firstName;
                existingData.LastName = lastName;
                existingData.PhoneNumber = phoneNumber;

                // Serialize the updated object back to JSON
                string updatedJson = JsonSerializer.Serialize(existingData);

                // Write the updated JSON back to the file
                File.WriteAllText(filePath, updatedJson);
            }
        }

        public static HeroData Sidekick2ReadHeroData()
        {
            var path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            string filePath = projectPath.ToString() + "Data\\API\\TestData\\Sidekick2Herodata.json";
            HeroData data = null;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                data = JsonSerializer.Deserialize<HeroData>(json);
            }

            return data;
        }
    }
}
