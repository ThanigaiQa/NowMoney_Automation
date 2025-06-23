using KARE.E2E.AUTOMATION.Data.API;
using KARE.E2E.AUTOMATION.Helpers;
using KARE.E2E.AUTOMATION.PageObjects.API.Models.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;
using RestSharp;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using static KARE.E2E.AUTOMATION.Models.Request.ShiftCheckOutRequest;

namespace KARE.E2E.AUTOMATION.PageObjects.API
{
    public class ShiftProcess
    {
        #region Declaration
        private IWebDriver _driver;
        private SeleniumActions seleniumActions;
        private Utilities utility;
        private String baseUrl;
        private RestClient client;
        public ScenarioContext scenarioContext;
        #endregion

        #region constructor
        public ShiftProcess(IWebDriver driver, ScenarioContext scenariocontext)
        {
            _driver = driver;
            utility = new Utilities();
            baseUrl = ConfigHelper.GetAPIBaseURL();
            client = new RestClient(baseUrl);
            seleniumActions = new SeleniumActions(_driver);
            scenarioContext = scenariocontext;
        }
        #endregion

        #region APIActions

        /// <summary>
        /// Obtain the total count of all shifts , Hero name and Tenant name.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        public void GetAllShifts(String bearerToken)
        {
            //****** Get All Shifts Request ******//

            var getall_shifts_request = new RestRequest(Endpoints.GET_POSTED_SHIFT, Method.Post);
            getall_shifts_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var requestbody = new
            {
                skipCount = 0,
                maxResultCount = 15,
                heroRoleName = "",
                tenantName = ""
            };

            getall_shifts_request.RequestFormat = DataFormat.Json;
            getall_shifts_request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            /******Get All Shifts Response******/

            var getall_shifts_response = client.Execute(getall_shifts_request);
            scenarioContext["APIResponse"] = getall_shifts_response.Content;
            Assert.IsTrue(getall_shifts_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            
        }

        /// <summary>
        /// Publish a shift using the designated shift ID
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="ShiftId">Get the Shift Id from the Get All Shifts API</param>
        public void PostShift(String bearerToken, String ShiftId)
        {
            //****** PostShift Request ******//

            var post_shifts_request = new RestRequest(Endpoints.APPLY_SHIFT, Method.Post);
            post_shifts_request.AddHeader("Authorization", $"Bearer {bearerToken}");
            post_shifts_request.AddQueryParameter("id", ShiftId);


            /****** PostShift Response******/

            var post_shifts_response = client.Execute(post_shifts_request);
            scenarioContext["APIResponse"] = post_shifts_response.Content;
            Assert.IsTrue(post_shifts_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(post_shifts_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Following the shift posting, the designated hero confirms the shift through the hero mobile app
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="ShiftId">Get the Shift Id from the Get All Shifts API</param>
        public void ShiftConfirm(String bearerToken, String ShiftId)
        {
            //****** Shift Confirm Request ******//

            var shift_confirm_request = new RestRequest(Endpoints.CONFIRM_SHIFT, Method.Put);
            shift_confirm_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var requestbody = new
            {
                shiftId = ShiftId,
                result = true
            };

            shift_confirm_request.RequestFormat = DataFormat.Json;
            shift_confirm_request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            /****** Shift Confirm Response******/

            var shift_confirm_response = client.Execute(shift_confirm_request);
            scenarioContext["APIResponse"] = shift_confirm_response.Content;
            Assert.IsTrue(shift_confirm_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(shift_confirm_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Following the shift confirm, the designated hero click the Ready to go button through the hero mobile app
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="ShiftId">Get the Shift Id from the Get All Shifts API</param>
        public void ShiftReadyToGo(String bearerToken, String ShiftId)
        {
            //****** ShiftReadyToGo Request ******//

            var shift_ready_request = new RestRequest(Endpoints.READY_TO_GO_SHIFT, Method.Put);
            shift_ready_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var requestbody = new
            {
                shiftId = ShiftId,
                result = true
            };

            shift_ready_request.RequestFormat = DataFormat.Json;
            shift_ready_request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            /****** ShiftReadyToGo Response******/

            var shift_ready_response = client.Execute(shift_ready_request);
            scenarioContext["APIResponse"] = shift_ready_response.Content;
            Assert.IsTrue(shift_ready_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(shift_ready_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Following the shift checkin, the designated hero click the Shift check in button through the hero mobile app
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="ShiftId">Get the Shift Id from the Get All Shifts API</param>
        public void ShiftCheckIn(String bearerToken, String ShiftId)
        {
            //****** ShiftCheckIn Request ******//

            var shift_checkin_request = new RestRequest(Endpoints.CHECK_IN_SHIFT, Method.Put);
            shift_checkin_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var requestbody = new
            {
                shiftId = ShiftId,
                result = true
            };

            shift_checkin_request.RequestFormat = DataFormat.Json;
            shift_checkin_request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            /****** ShiftCheckIn Response******/

            var shift_checkin_response = client.Execute(shift_checkin_request);
            scenarioContext["APIResponse"] = shift_checkin_response.Content;
            Assert.IsTrue(shift_checkin_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(shift_checkin_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Following the shift Check in, the designated hero click the Check Out Review button through the hero mobile app
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="ShiftId">Get the Shift Id from the Get All Shifts API</param>
        public void ShiftCheckOutReview(String bearerToken, String ShiftId)
        {
            // Define the Central Standard Time zone
            TimeZoneInfo centralTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Constants.CST_TIME);

            // Get the current time in the Central Time zone
            DateTime centralTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, centralTimeZone);

            // Format it in ISO 8601 format 
            string formattedUtcTime = centralTime.AddHours(7).ToString(Constants.ISO8601_TIMEFORMAT);

            //****** ShiftCheckOut Request ******//

            var shift_checkout_request = new RestRequest(Endpoints.CHECKOUT_SUBMIT_REVIEW, Method.Post);
            shift_checkout_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var shift1 = new Shiftrating()
            {
                reviewType = "10",
                rating = "5"
            };
            var shift2 = new Shiftrating()
            {
                reviewType = "20",
                rating = "5"
            };
            var shift3 = new Shiftrating()
            {
                reviewType = "30",
                rating = "5"
            };
            var shift4 = new Shiftrating()
            {
                reviewType = "40",
                rating = "5"
            };
            var shift5 = new Shiftrating()
            {
                reviewType = "50",
                rating = "5"
            };

            var ShiftRating = new Shiftrating[]
            {
                shift1,shift2,shift3,shift4,shift5
            };

            var requestbody = new
            {
                reviewComment = "Automated Check out",
                shiftId = ShiftId,
                shiftRatings = ShiftRating,
                checkOutTime = formattedUtcTime
            };

            shift_checkout_request.RequestFormat = DataFormat.Json;
            shift_checkout_request.AddJsonBody(requestbody);

            /****** ShiftCheckOut Response******/

            var shift_checkout_response = client.Execute(shift_checkout_request);
            scenarioContext["APIResponse"] = shift_checkout_response.Content;
            Assert.IsTrue(shift_checkout_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(shift_checkout_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }
        #endregion
    }
}
