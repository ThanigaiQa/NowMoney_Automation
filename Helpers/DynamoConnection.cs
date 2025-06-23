//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Amazon;
//using Amazon.DynamoDBv2;
//using Amazon.DynamoDBv2.DocumentModel;
//using Amazon.DynamoDBv2.Model;
//using Amazon.Runtime;

//namespace KARE.E2E.AUTOMATION.Helpers
//{
//    internal class DynamoConnection
//    {
//        private AmazonDynamoDBClient client;

//        /// <summary>
//        /// Method to Setup a DynamoDB Connetion
//        /// </summary>
//        private void SetupDynamoDBConnection()
//        {
//            var profile = "AUTO"; //Environment.GetEnvironmentVariable("AWS_PROFILE");
//            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
//            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");


//            if (!string.IsNullOrEmpty(profile) && profile != "default")
//            {
//                var credentials = new BasicAWSCredentials(accessKey,secretKey);

//                // Credentials to connect to AWS in Pipeline execution
//                client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
//                // Local credentials to connect to AWS
//                //client = new AmazonDynamoDBClient(cr, RegionEndpoint.USEast1);
//            }
//            else
//            {
//                client = new AmazonDynamoDBClient();
//            }
//        }

//        //TODO:
//        // Dynamically Change the EmailRecords table name based on the environment selected (See lines 54 and 56)
//        // We have the next tables:
//        // "AUT_EmailRecords"
//        // "Dev_EmailRecords"
//        // "UAT_EmailRecords"
//        // "Demo_EmailRecords"


//        /// <summary>
//        /// Setup a DynamoDB Connetion and return the Temporal passord sent to create a new Community Admin User
//        /// </summary>
//        /// <param name="communityEmailAddress">Any string value that is an email</param>
//        /// <returns>Temp Passowrd as string</returns>
//        public async Task<string> GetTempPasswordAsync(string communityEmailAddress)
//        {
//            //Notification Name
//            string notificationName = "Kare.ToCommunity.EmailActivation";

//            // Setting Up DynamoDB Connetion
//            this.SetupDynamoDBConnection();

//            //string environmentName = ConfigHelper.GetEnvironment();

//            //Table table = Table.LoadTable(this.client, environmentName + "_EmailRecords");

//            Table table = Table.LoadTable(this.client, "AUT_EmailRecords");

//            var search = new ScanFilter();

//            search.AddCondition("EmailAddress", ScanOperator.Contains, communityEmailAddress);
//            search.AddCondition("NotificationName", ScanOperator.Equal, notificationName);

//            var scanConfig = new ScanOperationConfig
//            {
//                Filter = search,
//                ConsistentRead = true
//            };

//            var searchQuery = table.Scan(scanConfig);

//            string? mailContent = "123qwe";

//            List<Document> results = new List<Document>();

//            int retryCount = 0;

//        retry:
//            results = await searchQuery.GetNextSetAsync();

//            while (retryCount < 60)
//            {
//                Thread.Sleep(5000);
//                if (results.Count == 0)
//                {
//                    retryCount++;
//                    goto retry;
//                }
//                goto extractresult;
//            }


//        extractresult:
//            foreach (var result in results)
//            {
//                string content = result["Content"];
//                //string password = new Utilities().ExtractPassword(content);
//                mailContent = content;
//            }

//            if (results.Count == 0)
//            {
//                mailContent = "No Results found";
//            }

//            return mailContent;
//        }
//    }
//}