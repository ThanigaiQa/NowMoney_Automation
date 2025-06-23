using KARE.E2E.AUTOMATION.Data.API;
using KARE.E2E.AUTOMATION.Helpers;
using KARE.E2E.AUTOMATION.Models.Request;
using KARE.E2E.AUTOMATION.Models.Response;
using KARE.E2E.AUTOMATION.PageObjects.API.Models.Request;
using KARE.E2E.AUTOMATION.PageObjects.API.Models.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;
using RestSharp;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using static KARE.E2E.AUTOMATION.Models.Request.HealthStatus;
using static KARE.E2E.AUTOMATION.Models.Request.HeroReference;
using static KARE.E2E.AUTOMATION.Models.Response.GetClubInfoResponse;
using static KARE.E2E.AUTOMATION.PageObjects.API.Models.Request.SaveDocumentRequest;

namespace KARE.E2E.AUTOMATION.PageObjects.API
{
    public class RegisterHero : ExtentReport
    {
        #region Declaration
        private IWebDriver _driver;
        private SeleniumActions seleniumActions;
        private Utilities utility;
        private String baseUrl;
        private RestClient client;
        public ScenarioContext scenarioContext;
        public FeatureContext featureContext;
        #endregion

        #region constructor
        public RegisterHero(ScenarioContext scenariocontext, FeatureContext featurecontext)
        {
            utility = new Utilities();
            baseUrl = ConfigHelper.GetAPIBaseURL();
            client = new RestClient(baseUrl);
            scenarioContext = scenariocontext;
            featureContext = featurecontext;
        }
        #endregion

        #region APIActions
        /// <summary>
        ///  Upload Applicant photo during the registration process on the mobile app
        /// </summary>
        /// <returns>File token of the image uploaded</returns>
        public String UploadHeroPhoto()
        {
            //-----------------Upload Photo Request------------------------------//
            var request = new RestRequest(Endpoints.UPLOAD_PHOTO, Method.Post);
            request.AddFile("fileName", utility.ProjectPath() + Constants.IMAGE_PATH);

            //----------------------------Execution-------------------------------------//

            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<UploadPhoto>(response.Content);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsNotNull(responseData);
            return responseData.result.fileToken;
        }

        /// <summary>
        /// Generate random data and utilize it for creating applicant accounts, including phone verification details.
        /// </summary>
        /// <param name="fileToken">fileToken that needs to be upload files</param>
        /// <returns>Bearer Token from phone verification API</returns>
        public String AccountAndPhoneVerification(String fileToken)
        {
            String result = "";
        //-------------------Create Random Data-------------------------------------//
        retryRandomData:
            try
            {
                var random_data_request = new RestRequest(Endpoints.RANDOM_DATA_URL, Method.Get);
                var random_data_response = client.Execute(random_data_request);
                Assert.IsTrue(random_data_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
                var random_data_deserialized = JsonConvert.DeserializeObject<RandomDataAlternate>(random_data_response.Content);


                //-----------------Account Verfication Request------------------------------//

                scenarioContext["PhoneNumber"] = utility.RandomPhoneNumber();
                var account_verfication_request = new RestRequest(Endpoints.VERIFY_ACCOUNT, Method.Post);
                account_verfication_request.AddQueryParameter("EmailAddress", random_data_deserialized.results[0].email);
                account_verfication_request.AddQueryParameter("FirstName", random_data_deserialized.results[0].name.first);
                account_verfication_request.AddQueryParameter("LastName", random_data_deserialized.results[0].name.last);
                account_verfication_request.AddQueryParameter("PhoneNumber", scenarioContext["PhoneNumber"].ToString());
                account_verfication_request.AddQueryParameter("Password", Constants.DEFAULT_PASSWORD);
                account_verfication_request.AddQueryParameter("PictureToken", fileToken);
                account_verfication_request.AddQueryParameter("AppVersion", ConfigHelper.GetAppVersion());

                //----------------------------Execution-------------------------------------//

                var account_verfication_response = client.Execute(account_verfication_request);
                Assert.IsTrue(account_verfication_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
                var responseData = JsonConvert.DeserializeObject<AccountVerification>(account_verfication_response.Content);
                Assert.IsNotNull(responseData);
                Assert.IsTrue(responseData.result.Equals(random_data_deserialized.results[0].email));
                Assert.IsTrue(responseData.success);
                scenarioContext["APIResponse"] = account_verfication_response.Content;

                //-----------------Phone Verfication Request------------------------------//

                var phone_verification_request = new RestRequest(Endpoints.PHONE_VERIFICATION, Method.Post);

                phone_verification_request.AddQueryParameter("code", Constants.DEFAULT_PHONECODE);
                phone_verification_request.AddQueryParameter("PictureToken", fileToken);
                phone_verification_request.AddQueryParameter("FirstName", random_data_deserialized.results[0].name.first);
                phone_verification_request.AddQueryParameter("LastName", random_data_deserialized.results[0].name.last);
                phone_verification_request.AddQueryParameter("EmailAddress", random_data_deserialized.results[0].email);
                phone_verification_request.AddQueryParameter("PhoneNumber", scenarioContext["PhoneNumber"].ToString());
                phone_verification_request.AddQueryParameter("Password", Constants.DEFAULT_PASSWORD);
                phone_verification_request.AddQueryParameter("HeroSocietyLoginType", "");
                phone_verification_request.AddQueryParameter("HeroSocietyLoginUserId", "");
                phone_verification_request.AddQueryParameter("DeviceType", Constants.DEFAULT_ANDROID_VERSION);

                scenarioContext["FirstName"] = random_data_deserialized.results[0].name.first;
                scenarioContext["LastName"] = random_data_deserialized.results[0].name.last;
                scenarioContext["FullName"] = scenarioContext["FirstName"] + " " + scenarioContext["LastName"];
                scenarioContext["EmailAddress"] = random_data_deserialized.results[0].email;

                //----------------------------Execution-------------------------------------//

                var phone_verfication_response = client.Execute(phone_verification_request);
                scenarioContext["APIResponse"] = phone_verfication_response.Content;
                Assert.IsTrue(phone_verfication_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
                var phone_verfication_responsedata = JsonConvert.DeserializeObject<AuthenticationResponse>(phone_verfication_response.Content);
                Assert.IsNotNull(phone_verfication_responsedata);
                Assert.IsTrue(phone_verfication_responsedata.success);
                result = phone_verfication_responsedata.result.accessToken;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                goto retryRandomData;
            }
            return result;
        }

        /// <summary>
        /// Post the personal information details of the applicant in the mobile app under "SavePersonalInfo" during the registration process.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        public void SavePersonalInfo(String bearerToken)
        {
            //-----------------Save Personal Info Request------------------------------//

            var personal_info_request = new RestRequest(Endpoints.SAVE_PERSONAL_INFO, Method.Post);
            personal_info_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            scenarioContext["SSN"] = utility.RandomSSN();

            var requestbody = new
            {
                state = Constants.STATE_ID["TEXAS"],
                ssn = scenarioContext["SSN"].ToString(),
                birthday = Constants.BIRTHDAY,
                address = Constants.ADDRESS,
                city = Constants.CITY,
                zipCode = Constants.ZIPCODE
            };

            personal_info_request.RequestFormat = DataFormat.Json;
            personal_info_request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            /******Save Personal Info Response******/

            var personal_info_response = client.Execute(personal_info_request);
            scenarioContext["APIResponse"] = personal_info_response.Content;
            Assert.IsTrue(personal_info_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(personal_info_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// GetTermsService API retrieves the terms and service document.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        public void GetTermsService(String bearerToken)
        {
            /****** Get Terms and Service Request ******/

            var get_terms_request = new RestRequest(Endpoints.TERMS_AND_SERVICE, Method.Get);
            get_terms_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            /****** Get Terms and Service Response ******/

            var get_terms_response = client.Execute(get_terms_request);
            scenarioContext["APIResponse"] = get_terms_response.Content;
            Assert.IsTrue(get_terms_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(get_terms_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// The applicant agree to the terms and conditions of Kare.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        public void AcceptTermsService(String bearerToken)
        {
            /****** Accept Terms and Service Request ******/

            var accept_terms_request = new RestRequest(Endpoints.ACCEPT_TERMS_OF_SERVICE, Method.Get);
            accept_terms_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            /****** Accept Terms and Service Response ******/

            var accept_terms_response = client.Execute(accept_terms_request);
            scenarioContext["APIResponse"] = accept_terms_response.Content;
            Assert.IsTrue(accept_terms_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(accept_terms_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Get the ID for necessary documents to use when calling the "GET_MISSING_DOCUMENTS" API.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        /// <returns>Get all the Documents Id as List<int></returns>
        public List<int> GetDocuments(String bearerToken)
        {
            /****** Get Documents Request ******/

            var get_documents_request = new RestRequest(Endpoints.GET_MISSING_DOCUMENTS, Method.Get);
            get_documents_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            /****** Get Documents  Response ******/

            var get_documents_response = client.Execute(get_documents_request);
            Assert.IsTrue(get_documents_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GetDocuments>(get_documents_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
            Assert.IsTrue(response.result.licenseType.Equals(Int32.Parse(scenarioContext["licenseType"].ToString())));
            Assert.IsTrue(response.result.licenseNumber.Equals(scenarioContext["licenseNumber"].ToString()));
            Assert.IsTrue(response.result.licenseDescription.Equals(scenarioContext["license"]));

            List<int> DocumentList = new List<int>();

            for (int i = 0; i < response.result.documentTypes.Length; i++)
            {
                DocumentList.Add(response.result.documentTypes[i].id);
            }
            return DocumentList;
        }

        /// <summary>
        /// Uploads essential documents to use when calling the "UPLOAD_DOCUMENT" API
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        /// <returns> fileToken as string </returns>
        public String UploadHeroFiles(String bearerToken)
        {
            //-----------------Upload Photo Request------------------------------//

            var request = new RestRequest(Endpoints.UPLOAD_DOCUMENT, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            request.AddFile("fileName", utility.ProjectPath() + Constants.IMAGE_PATH);

            //----------------------------Execution-------------------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<UploadPhoto>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            return responseData.result.fileToken;
        }

        /// <summary>
        /// Continue using this API with document IDs (acquired from the "GET_MISSING_DOCUMENTS" API) and file token until all documents are successfully uploaded
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        /// <param name="FileToken"></param>
        /// <param name="DocumentIds"></param>
        public void SaveHeroDocument(String bearerToken, String FileToken, List<int> DocumentIds, String stateName)
        {
            for (int i = 0; i < DocumentIds.Count; i++)
            {
                /****** Save Hero Document Request ******/
                var save_docuemnt_request = new RestRequest(Endpoints.SAVE_DOCUMENT, Method.Post);
                save_docuemnt_request.AddHeader("Authorization", $"Bearer {bearerToken}");

                var file = new Filelist()
                {
                    fileToken = FileToken,
                    fileName = "KARE.jpeg",
                    fileType = "image/jpeg",
                    code = 0,
                    message = null,
                    details = null,
                    validationErrors = null,
                    newUp = true
                };

                var fileLists = new Filelist[]
                {
                file
                };

                var requestbody = new SaveDocumentRequest
                {
                    fileList = fileLists,
                    expiredDate = "",
                    documentTypeId = DocumentIds[i],
                    IsRegistered = true,
                    stateId = int.Parse(Constants.STATE_ID[stateName])
                };

                save_docuemnt_request.RequestFormat = DataFormat.Json;
                save_docuemnt_request.AddJsonBody(requestbody);
                scenarioContext["APIRequest"] = requestbody.ToJson();

                /****** Save Hero Document Response ******/

                var save_docuemnt_response = client.Execute(save_docuemnt_request);
                scenarioContext["APIResponse"] = save_docuemnt_response.Content;
                Assert.IsTrue(save_docuemnt_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
                var response = JsonConvert.DeserializeObject<GeneralResponse>(save_docuemnt_response.Content);
                Assert.IsTrue(response.success);
                Assert.IsTrue(response.__abp);
            }
        }

        /// <summary>
        /// After the applicant has successfully uploaded all required documents through the mobile app, they are required to submit the entire set of documents for the review process
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        public void DocumentSubmitForReview(String bearerToken)
        {
            /****** Document Submit For Review Request ******/

            var get_terms_request = new RestRequest(Endpoints.SUBMIT_DOCUMENTS, Method.Get);
            get_terms_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            /****** Document Submit For Review Response ******/

            var get_terms_response = client.Execute(get_terms_request);
            scenarioContext["APIResponse"] = get_terms_response.Content;
            Assert.IsTrue(get_terms_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(get_terms_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Provide the bank details while going through the registration process on the mobile app
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        public void UpdateHeroBankAccount(String bearerToken)
        {
            /****** Update Hero Bank Account Request ******/

            var update_hero_request = new RestRequest(Endpoints.UPDATE_HERO_BANK_ACCOUNT, Method.Put);
            update_hero_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            scenarioContext["bankRoutingNumber"] = utility.RandomNumberGenerator(9);
            scenarioContext["bankAccountNumber"] = utility.RandomNumberGenerator(10);
            var requestbody = new
            {
                paymentPersonalName = "Rohit 123",
                bankRoutingNumber = scenarioContext["bankRoutingNumber"].ToString(),
                bankAccountType = Constants.BANK_ACCOUNT_TYPE["CHECKING"],
                bankAccountNumber = scenarioContext["bankAccountNumber"].ToString()
            };

            update_hero_request.RequestFormat = DataFormat.Json;
            update_hero_request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            /****** Update Hero Bank Account Response ******/

            var update_hero_response = client.Execute(update_hero_request);
            scenarioContext["APIResponse"] = update_hero_response.Content;
            Assert.IsTrue(update_hero_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(update_hero_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Upload the Applicant resume in the mobile app during the registration process
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        /// <returns>File token as string</returns>
        public String UploadHeroResume(String bearerToken)
        {
            //-----------------Upload Resume Request------------------------------//

            var request = new RestRequest(Endpoints.UPLOAD_RESUME, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            request.AddFile("fileName", utility.ProjectPath() + Constants.IMAGE_PATH);

            //-----------------Upload Resume Response----------------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<UploadHeroResume>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            return responseData.result[0].fileToken;
        }

        /// <summary>
        /// Successfully upload the Resume in the mobile app during the registration process
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        public void CompleteUploadResume(String bearerToken)
        {
            //-----------------Complete Upload Resume Request------------------------------//

            var request = new RestRequest(Endpoints.SUBMIT_RESUME, Method.Get);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            //----------------Complete Upload Resume Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GeneralResponse>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
        }

        /// <summary>
        /// Upload the Tax Form in the mobile app during the registration process
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        /// <returns>fileToken as string</returns>
        public String PreviewPDF(String bearerToken)
        {
            //-----------------Preview PDF Request------------------------------//

            var request = new RestRequest(Endpoints.REVIEW_TAX_FORM, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            //----------------Preview PDF Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<UploadPhoto>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            return responseData.result.fileToken;
        }

        /// <summary>
        /// Sign the tax form using the mobile app while registering
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Account and Phone Verification API</param>
        /// <param name="PDFToken">Get the PDFToken from the PreviewPDF API</param>
        public void SignPDFForm(String bearerToken, String PDFToken)
        {
            //-----------------Sign PDF Request------------------------------//

            var request = new RestRequest(Endpoints.SIGN_TAX_FORM, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var requestbody = new
            {
                signPicBase64 = Constants.SIGNPICBASE64,
                fileToken = PDFToken
            };

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestbody);

            //----------------Sign PDF Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GeneralResponse>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
        }

        /// <summary>
        /// The authenticate API facilitates user login within the Hero mobile app.
        /// </summary>
        /// <param name="Username">Update the Hero's UserName</param>
        /// <param name="Password">Update the Hero's Password</param>
        /// <returns>accessToken as string</returns>
        public string Authenticate(String Username, String Password)
        {
            //****************** Authenticate Request *************************//

            var request = new RestRequest(Endpoints.AUTH, Method.Post);

            var requestbody = new
            {
                userNameOrEmailAddress = Username,
                password = Password,
                appVersion = ConfigHelper.GetAppVersion()
            };

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            //****************** Authenticate Response *************************//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<AuthenticationResponse>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            return responseData.result.accessToken;
        }

        /// <summary>
        /// Retrieve the personal information of a hero from the mobile app once the applicant has successfully become a hero.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        public void GetPersonInfo(String bearerToken)
        {
            //-----------------Get Person Info Request------------------------------//

            var request = new RestRequest(Endpoints.GET_PERSONAL_INFO, Method.Get);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            //----------------Get Person Info Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetPersonInfoResponse>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            Assert.IsTrue(responseData.__abp);//"userId": 10002297,
            Assert.IsTrue(responseData.__abp);//"userName": "merlinej@gmail.com",
            Assert.IsTrue(responseData.__abp);//"emailAddress": "merlinej@gmail.com",
            Assert.IsTrue(responseData.__abp);//"firstName": "Merlin",
            Assert.IsTrue(responseData.__abp);//"phoneNumber": "3454647651",
            Assert.IsTrue(responseData.__abp);//"ssn": "123-56-1243",
        }

        /// <summary>
        /// Retrieve the respective market id's of a hero from the mobile app once the applicant has successfully become a hero
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <returns>Market Id as List<Int></returns>
        public List<int> GetHeroMarkets(String bearerToken)
        {
            List<int> MarketList = new List<int>();
            //-----------------Get Hero Markets Request------------------------------//

            var request = new RestRequest(Endpoints.GET_HERO_MARKETS, Method.Get);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            //----------------Get Hero Markets Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetHeroMarkets>(response.Content);
            Assert.IsNotNull(responseData);

            for (int i = 0; i < responseData.result.onboardMarketGroups.Length; i++)
            {
                if (responseData.result.onboardMarketGroups[i].stateId.Equals(43))
                {
                    for (int j = 0; j < responseData.result.onboardMarketGroups[i].markets.Length; j++)
                    {
                        MarketList.Add(responseData.result.onboardMarketGroups[i].markets[j].marketId);
                    }
                }
            }
            return MarketList;
        }

        /// <summary>
        /// After acquiring market IDs, the hero has the capability to modify the market information associated with their profile.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="MarketIdsList">Get Market list id from the Get Hero Markets API</param>
        public void UpdateHeroMarketPatch(String bearerToken, List<int> MarketIdsList)
        {
            //----------------Update Hero Market Patch Request------------------------------//

            var request = new RestRequest(Endpoints.UPDATE_HERO_MARKETS, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");


            int[] marketIds = MarketIdsList.ToArray();

            var requestbody = new
            {
                marketIds = marketIds
            };

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            //----------------Update Hero Market Patch Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GeneralResponse>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
        }

        /// <summary>
        /// Upon updating their market information, the hero has effectively completed the registration process for the Kare Hero app.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        public void CompleteRegister(String bearerToken)
        {
            //-----------------Complete Hero Register Request------------------------------//

            var request = new RestRequest(Endpoints.COMPLETE_HERO_REGISTER, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            //----------------Complete Hero Register Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GeneralResponse>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
        }

        /// <summary>
        /// Post the details of the applicant's license information(This information could differ based on the state)in the mobile app during the registration process.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="stateID">Get State IDs from Constants</param>
        /// <param name="licenseType">Get License Type from Constants</param>
        public void CreateHeroLicenseInfoStates(String bearerToken, string stateID, string licenseType)
        {
            /****** Create Hero LicenseInfo Request ******/

            var hero_license_request = new RestRequest(Endpoints.CREATE_HERO_LICENSE, Method.Post);
            hero_license_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            scenarioContext["licenseNumber"] = utility.RandomNumberGenerator(10);
            scenarioContext["licenseType"] = Constants.LICENSE_TYPE[licenseType];
            scenarioContext["license"] = licenseType;
            var requestbody = new
            {
                subLicenseType = "",
                licenseType = scenarioContext["licenseType"],
                stateId = Constants.STATE_ID[stateID],
                licenseNumber = scenarioContext["licenseNumber"].ToString(),
            };

            hero_license_request.RequestFormat = DataFormat.Json;
            hero_license_request.AddJsonBody(requestbody);

            /****** Create Hero LicenseInfo Response ******/

            var hero_license_response = client.Execute(hero_license_request);
            Assert.IsTrue(hero_license_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(hero_license_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Upload the Applicant's BID Form in the mobile app during the registration process
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <returns>file Token as String</returns>
        public String UploadBIDForm(String bearerToken)
        {
            //-----------------Upload BID Form------------------------------//

            var request = new RestRequest(Endpoints.PREVIEW_BID_FORM, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            var requestbody = new
            {
                race = "3",
                gender = "0",
                isCheckSelectionA1 = "false",
                isCheckSelectionA2 = "false",
                isCheckSelectionA3 = "false",
                isCheckSelectionA4 = "false",
                isCheckSelectionA5 = "false",
                isCheckSelectionA6 = "false",
                isCheckSelectionA7 = "false",
                isCheckSelectionB1 = "false",
                isCheckSelectionB2 = "false",
                isCheckSelectionB3 = "false",
                isCheckSelectionB4 = "false",
                isCheckSelectionB5 = "false",
                isCheckSelectionB6 = "false",
                isCheckSelectionB7 = "false",


            };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestbody);

            /****** Preview BID Form Response ******/

            var bid_response = client.Execute(request);
            Assert.IsTrue(bid_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<UploadPhoto>(bid_response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            return responseData.result.fileToken;

        }

        /// <summary>
        /// Sign the BID Form using the mobile app while registering
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="formToken">Get the file token from the Upload BID Form API</param>
        public void SignBIDForm(String bearerToken, String formToken)
        {
            //****** Sign BID Form ******//

            var request = new RestRequest(Endpoints.SIGN_BID_FORMV2, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var requestbody = new
            {
                signPicBase64 = Constants.SIGNPICBASE64,
                fileToken = formToken,
                stateId = "null"
            };

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestbody);

            // ****** Sign BID Form Response ****** //

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GeneralResponse>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
        }

        /// <summary>
        /// Upload the Applicant's Health Status Physical Exam in the mobile app during the registration process
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <returns>file token as string</returns>
        public String UploadHealthStatusPhysicalExam(String bearerToken)
        {
            //****** UPLOAD HEALTH STATUS PHYSICAL EXAM ******//

            var request = new RestRequest(Endpoints.PREVIEW_HEALTH_STATUS_PHYSICALEXAM_FORM, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            String[] medicalcon = new string[]
            {
                Constants.MEDICALCONDITIONS_INDIANA
            };

            var file = new Medicationnamereason()
            {
                medicationName = Constants.TESTNAME,
                medicationReason = Constants.TESTNAME
            };

            var Medicationnamereasons = new Medicationnamereason[]
                {
                file
                };

            var requestbody = new HealthStatus()
            {
                height = Constants.HEALTHSTATUS_HEIGHT,
                weight = Constants.HEALTHSTATUS_WEIGHT,
                historyOfSeriousHealthIssue = true,
                historyOfSeriousHealthIssueDescription = Constants.TESTNAME,
                currentIssueToJobDuties = true,
                currentIssueToJobDutiesDescription = Constants.TESTNAME,
                selectedMedicalConditions = medicalcon,
                otherMedicalConditionChecked = true,
                otherMedicalConditionDescription = Constants.TESTNAME,
                medicationNameReasons = Medicationnamereasons
            };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestbody);

            /****** PREVIEW HEALTH STATUS PHYSICAL EXAM ******/

            var health_response = client.Execute(request);
            Assert.IsTrue(health_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<PhysicalHealthStatusExam>(health_response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            return responseData.result.fileToken;
        }

        /// <summary>
        /// Sign the Health Status Physical Exam using the mobile app while registering
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="formToken">Get the file token from the Upload Health Status Physical Exam API</param>
        public void SignHealthStatusPhysicalExam(String bearerToken, String formToken)
        {
            //****** SIGN HEALTH STATUS PHYSICAL EXAM REQUEST******//
            var request = new RestRequest(Endpoints.SIGN_HEALTH_STATUS_PHYSICALEXAM_FORM, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var requestbody = new
            {
                signPicBase64 = Constants.SIGNPICBASE64,
                fileToken = formToken,
                stateId = Constants.STATE_ID["INDIANA"]
            };

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestbody);

            //****** SIGN HEALTH STATUS PHYSICAL EXAM RESPONSE ******//
            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
        }

        /// <summary>
        /// Enter the reference information for the hero.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        public void SubmitHeroReference1(String bearerToken, String stateName)
        {
            /****** Submit Hero Reference Request ******/

            var hero_reference_request = new RestRequest(Endpoints.SUBMIT_HERO_REFERENCE1, Method.Post);
            hero_reference_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var heroref = new Heroreference()
            {
                
                name = Constants.HEROREFERENCE_NAME,
                emailOrPhone = Constants.HEROREFERENCE_EMAIL,
                relationship = Constants.HEROREFERENCE_RELATION
            };

            var Heroreferences = new Heroreference[]
                {
                    heroref,
                    heroref,
                    heroref
                };

            var requestbody = new
            {
                stateId = int.Parse(Constants.STATE_ID[stateName]),
                heroReferences = Heroreferences
            };

            hero_reference_request.RequestFormat = DataFormat.Json;
            hero_reference_request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            /****** Submit Hero Reference Response ******/

            var hero_reference_response = client.Execute(hero_reference_request);
            scenarioContext["APIResponse"] = hero_reference_response.Content;
            Assert.IsTrue(hero_reference_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);

        }

        /// <summary>
        /// Enter the reference information for the hero.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        public void SubmitHeroReference2(String bearerToken)
        {
            /****** Submit Hero Reference2 Request ******/

            var hero_reference_request = new RestRequest(Endpoints.SUBMIT_HERO_REFERENCE2, Method.Post);
            hero_reference_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var heroref = new Heroreference()
            {
                name = Constants.HEROREFERENCE_NAME,
                emailOrPhone = Constants.HEROREFERENCE_EMAIL,
                relationship = Constants.HEROREFERENCE_RELATION
            };

            var Heroreferences = new Heroreference[]
                {
                    heroref
                };

            var requestbody = new
            {
                heroReferences = Heroreferences
            };

            hero_reference_request.RequestFormat = DataFormat.Json;
            hero_reference_request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            /****** Submit Hero Reference2 Response ******/

            var hero_reference_response = client.Execute(hero_reference_request);
            scenarioContext["APIResponse"] = hero_reference_response.Content;
            Assert.IsTrue(hero_reference_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);

        }

        /// <summary>
        /// Upload the Gchex form in the mobile app during the registration process
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <returns> file Token as string </returns>
        public String PreviewGchex(String bearerToken)
        {
            //-----------------Preview Gchex Request------------------------------//

            var request = new RestRequest(Endpoints.PREVIEW_GCHEX, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            //----------------Preview Gchex Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<UploadPhoto>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            return responseData.result.fileToken;
        }

        /// <summary>
        /// Sign the Gchex Form using the mobile app while registering
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="PDFToken">Get the PDF token from the Preview Gchex API</param>
        public void SignGchex(String bearerToken, String PDFToken)
        {
            //-----------------Sign Gchex Request------------------------------//

            var request = new RestRequest(Endpoints.SIGN_GCHEX, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var requestbody = new
            {
                signPicBase64 = Constants.SIGNPICBASE64,
                fileToken = PDFToken
            };

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestbody);

            //----------------Sign Gchex Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;  
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GeneralResponse>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
        }

        /// <summary>
        /// Update the hero's live states..
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        public void UpdateLiveInTheState(String bearerToken)
        {
            //-----------------Update Live in the state status------------------------------//

            var request = new RestRequest(Endpoints.UPDATE_LIVE_IN_THE_STATE, Method.Post);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var requestbody = new
            {
                isLiveInTheStateRecently = "true"
            };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(requestbody);

            //----------------Update Live in the state status Response-------------------------//

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GeneralResponse>(response.Content);
            Assert.IsNotNull(responseData);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);



        }

        /// <summary>
        /// Retrieve the most recent onboarding status of the hero.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="stateID">Get State IDs from Constants</param>
        public void GetLastOnboardingState(String bearerToken, String stateID)
        {
            /****** Get Last Onboarding State Request ******/

            var get_state_request = new RestRequest(Endpoints.GET_LAST_ONBOARDING_STATE, Method.Get);
            get_state_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            /****** Get Last Onboarding State Response ******/

            var get_state_response = client.Execute(get_state_request);
            scenarioContext["APIResponse"] = get_state_response.Content;
            Assert.IsTrue(get_state_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetStateID>(get_state_response.Content);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            int stateIDValue = responseData.result.stateId;
            String stateId2 = Constants.STATE_ID[stateID];
            Assert.IsTrue(Constants.STATE_ID[stateID].Equals(stateIDValue.ToString()));
        }

        /// <summary>
        /// Enroll the hero in an additional state
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        public void GetCanRegisterAnotherState(String bearerToken)
        {
            /****** Get Can Register Another State Request ******/

            var get_state_request = new RestRequest(Endpoints.GET_CAN_REISTER_ANOTHER_STATE, Method.Get);
            get_state_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            /****** Get Can Register Another State Response ******/

            var get_state_response = client.Execute(get_state_request);
            scenarioContext["APIResponse"] = get_state_response.Content;
            Assert.IsTrue(get_state_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetCanRegisterAnotherState>(get_state_response.Content);
            Assert.IsTrue(responseData.success);
            Assert.IsTrue(responseData.__abp);
            bool value = responseData.result.canRegister;
            Assert.IsTrue(value.Equals(true));
        }

        /// <summary>
        /// Post the details of the applicant's license information(This information could differ based on the state)in the mobile app during the registration process.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="stateID">Get State IDs from Constants</param>
        /// <param name="licenseType">Get License type from Constants</param>
        public void CreateHeroLicenseInfoV2(String bearerToken, string stateID, string licenseType)
        {
            /****** Create Hero LicenseInfo Request ******/

            var hero_license_request = new RestRequest(Endpoints.CREATE_HERO_LICENSE, Method.Post);
            hero_license_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            scenarioContext["licenseNumber"] = utility.RandomNumberGenerator(10);
            scenarioContext["licenseType"] = Constants.LICENSE_TYPE[licenseType];
            scenarioContext["license"] = licenseType;
            var requestbody = new
            {
                licenseType = scenarioContext["licenseType"],
                stateId = Constants.STATE_ID[stateID],
            };

            hero_license_request.RequestFormat = DataFormat.Json;
            hero_license_request.AddJsonBody(requestbody);

            /****** Create Hero LicenseInfo Response ******/

            var hero_license_response = client.Execute(hero_license_request);
            Assert.IsTrue(hero_license_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(hero_license_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Retrieve the respective market id's of a hero from the mobile app once the applicant has successfully become a hero
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <returns>Market Ids as list</Int></returns>
        public List<int> GetHeroMarketsForNewAddedState(String bearerToken)
        {
            List<int> MarketList = new List<int>();
            //-----------------Get Hero Markets For New Added State Request------------------------------//

            var request = new RestRequest(Endpoints.GET_HERO_MARKETS, Method.Get);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            //----------------Get Hero Markets For New Added State Response-------------------------//

            var response = client.Execute(request);
            scenarioContext["APIResponse"] = response.Content;  
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetHeroMarketsForNewAddedState>(response.Content);
            Assert.IsNotNull(responseData);

            for (int i = 0; i < responseData.result.onboardMarketGroups.Length; i++)
            {
                if (responseData.result.onboardMarketGroups[i].stateId.Equals(21))
                {
                    for (int j = 0; j < responseData.result.onboardMarketGroups[i].markets.Length; j++)
                    {
                        MarketList.Add(responseData.result.onboardMarketGroups[i].markets[j].marketId);
                    }
                }
                else if (responseData.result.onboardMarketGroups[i].stateId.Equals(43))
                {
                    for (int j = 0; j < responseData.result.onboardMarketGroups[i].markets.Length; j++)
                    {
                        MarketList.Add(responseData.result.onboardMarketGroups[i].markets[j].marketId);
                    }
                }
            }
            return MarketList;
        }

        /// <summary>
        /// Get the ID for necessary documents to use when calling the "GET_MISSING_DOCUMENTS" API
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <returns>Document Ids as list<int></returns>
        public List<int> GetDocumentsForMultiState(String bearerToken)
        {
            /****** Get Documents Request ******/

            var get_documents_request = new RestRequest(Endpoints.GET_MISSING_DOCUMENTS, Method.Get);
            get_documents_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            /****** Get Documents  Response ******/

            var get_documents_response = client.Execute(get_documents_request);
            Assert.IsTrue(get_documents_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GetDocuments>(get_documents_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);

            List<int> DocumentList = new List<int>();

            for (int i = 0; i < response.result.documentTypes.Length; i++)
            {
                DocumentList.Add(response.result.documentTypes[i].id);
            }
            return DocumentList;
        }

        /// <summary>
        /// Upon a hero inviting one of their contacts or extending an invitation to someone, an invite ID is generated
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <returns>Get Invite Id as String</returns>
        public String GetInviteId(String bearerToken)
        {
            //-----------------Invite Id Request------------------------------//
            var get_invite_id = new RestRequest(Endpoints.GET_INVITE_ID, Method.Get);
            get_invite_id.AddHeader("Authorization", $"Bearer {bearerToken}");
            get_invite_id.AddQueryParameter("invitesCount", 1);

            //----------------------------Execution-------------------------------------//

            var response = client.Execute(get_invite_id);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetInviteId>(response.Content);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsNotNull(responseData);
            return responseData.result[0];
        }

        /// <summary>
        /// Upon a hero send invite for one of their contacts or extending an invitation to someone.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="inviteId">Get the Invite id from the Get Invite Id API</param>
        public void SendInvite(String bearerToken, String inviteId)
        {
            /****** Send Invite Request ******/

            var send_invite_request = new RestRequest(Endpoints.SEND_INVITE, Method.Post);
            send_invite_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            scenarioContext["phoneno"] = utility.RandomPhoneNumber();
            scenarioContext["firstname"] = utility.GenerateRandomText(4);
            scenarioContext["lastname"] = utility.GenerateRandomText(4);

            var requestbody = new List<object>
            {
                new
                {
                    promotionId = 1,
                    firstName = scenarioContext["firstname"].ToString(),
                    lastName = scenarioContext["lastname"].ToString(),
                    phoneNumber = scenarioContext["phoneno"].ToString(),
                    inviteCode = inviteId,
                    message = "string"

                }
            };

            send_invite_request.RequestFormat = DataFormat.Json;
            send_invite_request.AddJsonBody(requestbody);
            scenarioContext["APIRequest"] = requestbody.ToJson();

            /****** Send Invite Response ******/

            var send_invite_response = client.Execute(send_invite_request);
            scenarioContext["APIResponse"] = send_invite_response.Content;
            Assert.IsTrue(send_invite_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(send_invite_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// After a hero invites their friends, the mobile numbers should undergo a sanitization process
        /// </summary>
        /// <param name="bearerToken">Get the Invite id from the Get Invite Id API</param>
        public void SantizeInvitePhoneNumber(String bearerToken)
        {
            /****** Santize Invite PhoneNumber Request ******/

            var santize_invite_request = new RestRequest(Endpoints.SANTIZE_INVITE_PHONENUMBER, Method.Post);
            santize_invite_request.AddHeader("Authorization", $"Bearer {bearerToken}");

            santize_invite_request.RequestFormat = DataFormat.Json;


            /****** Santize Invite PhoneNumber Response ******/

            var send_invite_response = client.Execute(santize_invite_request);
            Assert.IsTrue(send_invite_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(send_invite_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Upon a hero successfully inviting their friends, the status will be shown adjacent to the invited profiles
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="inviteId">Get the Invite id from the Get Invite Id API</param>
        public void UpdateStatus(String bearerToken, String inviteId)
        {
            //-----------------Update Status Request------------------------------//
            var update_status_request = new RestRequest(Endpoints.UPDATE_STATUS, Method.Post);
            update_status_request.AddHeader("Authorization", $"Bearer {bearerToken}");
            update_status_request.AddQueryParameter("inviteId", inviteId);

            //----------------------------Execution-------------------------------------//

            var update_status_reponse = client.Execute(update_status_request);

            Assert.IsTrue(update_status_reponse.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(update_status_reponse.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Upon a hero successfully inviting their friends, the status will be shown adjacent to the invited profiles
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="inviteid">Get the Invite id from the Get Invite Id API</param>
        public void UpdateStatusV2(String bearerToken, String inviteid)
        {
            //-----------------Update Status Request------------------------------//
            var update_status_request = new RestRequest(Endpoints.UPDATE_STATUS_V2, Method.Post);
            update_status_request.AddHeader("Authorization", $"Bearer {bearerToken}");


            var requestbody = new
            {
                inviteId = inviteid,
                status = 1
            };

            update_status_request.RequestFormat = DataFormat.Json;
            update_status_request.AddJsonBody(requestbody);

            //----------------------------Execution-------------------------------------//

            var update_status_reponse = client.Execute(update_status_request);

            Assert.IsTrue(update_status_reponse.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(update_status_reponse.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }

        /// <summary>
        /// Retrieve the performance details for the Hero and Sidekick of the month, including shift completion and Karecash information.
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="status">Get the Status from the Update Status API</param>
        public void GetClubInfo(String bearerToken, String status)
        {
            //-----------------Club Info Request------------------------------//
            var get_club_info = new RestRequest(Endpoints.GET_CLUB_INFO, Method.Get);
            get_club_info.AddHeader("Authorization", $"Bearer {bearerToken}");


            //----------------------------Execution-------------------------------------//

            var response = client.Execute(get_club_info);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetClubInfoResponse>(response.Content);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsNotNull(responseData);
            if (status == "Referral Made")
            {

                for (int i = 0; i < responseData.result.sidekicksPerformance.Length; i++)
                {
                    Sidekicksperformance sidekick = responseData.result.sidekicksPerformance[i];
                    var sidekickName = sidekick.name;
                    scenarioContext["fullname"] = scenarioContext["firstname"] + " " + scenarioContext["lastname"];
                    if (sidekickName == scenarioContext["fullname"].ToString())
                    {
                        var inviteStatus = responseData.result.sidekicksPerformance[i].inviteStatus;
                        Assert.IsTrue(inviteStatus.ToString().Equals(Constants.REFERRAL_MADE_STATUS));
                        break;
                    }
                }



            }
            else if (status == "In-Pipeline")
            {
                for (int i = 0; i < responseData.result.sidekicksPerformance.Length; i++)
                {
                    Sidekicksperformance sidekick = responseData.result.sidekicksPerformance[i];
                    var sidekickName = sidekick.name;
                    scenarioContext["fullname"] = scenarioContext["firstname"] + " " + scenarioContext["lastname"];
                    if (sidekickName == scenarioContext["fullname"].ToString())
                    {
                        Assert.IsTrue(responseData.result.sidekicksPerformance[i].inviteStatus.ToString().Equals(Constants.IN_PIPELINE_STATUS));
                        break;
                    }
                }

            }
            else if (status == "Hero")
            {
                for (int i = 0; i < responseData.result.sidekicksPerformance.Length; i++)
                {
                    Sidekicksperformance sidekick = responseData.result.sidekicksPerformance[i];
                    var sidekickName = sidekick.name;
                    scenarioContext["fullname"] = ConfigHelper.Sidekick1ReadHeroData().FirstName + " " + ConfigHelper.Sidekick1ReadHeroData().LastName;
                    if (sidekickName == scenarioContext["fullname"].ToString())
                    {
                        Assert.IsTrue(responseData.result.sidekicksPerformance[i].inviteStatus.ToString().Equals(Constants.HERO_STATUS));
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// Obtain the names of the Super Hero and their Sidekick
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        /// <param name="superheroname">Get the Names from the Get Club Info API</param>
        /// <param name="superfriendname">Get the Names from the Get Club Info API</param>
        public void GetClubInfoNames(String bearerToken, String superheroname, String superfriendname)
        {
            //-----------------Club Info Request------------------------------//
            var get_club_info = new RestRequest(Endpoints.GET_CLUB_INFO, Method.Get);
            get_club_info.AddHeader("Authorization", $"Bearer {bearerToken}");


            //----------------------------Execution-------------------------------------//

            var response = client.Execute(get_club_info);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetClubInfoResponse>(response.Content);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsNotNull(responseData);
            var superHero = responseData.result.superHeroAndFriends.superhero.name;
            Assert.IsTrue(superHero.Equals(superheroname));
            var superFriend = responseData.result.superHeroAndFriends.superfriends[0].name;
            Assert.IsTrue(superFriend.Equals(superfriendname));

        }

        /// <summary>
        /// Generate random data and utilize it for creating applicant accounts, including phone verification details
        /// </summary>
        /// <param name="fileToken">fileToken that needs to be upload files</param>
        /// <returns>access Token as string</returns>
        public String SidekickAccountAndPhoneVerification(String fileToken)
        {
            String result = "";
        //-------------------Create Random Data-------------------------------------//
        retryRandomData:
            try
            {
                var random_data_request = new RestRequest(Endpoints.RANDOM_DATA_URL, Method.Get);
                var random_data_response = client.Execute(random_data_request);
                Assert.IsTrue(random_data_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
                var random_data_deserialized = JsonConvert.DeserializeObject<RandomDataAlternate>(random_data_response.Content);


                //-----------------Account Verfication Request------------------------------//

                var account_verfication_request = new RestRequest(Endpoints.VERIFY_ACCOUNT, Method.Post);
                account_verfication_request.AddQueryParameter("EmailAddress", random_data_deserialized.results[0].email);
                account_verfication_request.AddQueryParameter("FirstName", random_data_deserialized.results[0].name.first);
                account_verfication_request.AddQueryParameter("LastName", random_data_deserialized.results[0].name.last);
                account_verfication_request.AddQueryParameter("PhoneNumber", scenarioContext["phoneno"].ToString());
                account_verfication_request.AddQueryParameter("Password", Constants.DEFAULT_PASSWORD);
                account_verfication_request.AddQueryParameter("PictureToken", fileToken);
                account_verfication_request.AddQueryParameter("AppVersion", ConfigHelper.GetAppVersion());

                //----------------------------Execution-------------------------------------//

                var account_verfication_response = client.Execute(account_verfication_request);
                Assert.IsTrue(account_verfication_response.StatusCode == System.Net.HttpStatusCode.OK, "Error Response Received");
                var responseData = JsonConvert.DeserializeObject<AccountVerification>(account_verfication_response.Content);
                Assert.IsNotNull(responseData);
                Assert.IsTrue(responseData.result.Equals(random_data_deserialized.results[0].email));
                Assert.IsTrue(responseData.success);
                scenarioContext["APIResponse"] = account_verfication_response.Content;

                //-----------------Phone Verfication Request------------------------------//

                var phone_verification_request = new RestRequest(Endpoints.PHONE_VERIFICATION, Method.Post);

                phone_verification_request.AddQueryParameter("code", Constants.DEFAULT_PHONECODE);
                phone_verification_request.AddQueryParameter("PictureToken", fileToken);
                phone_verification_request.AddQueryParameter("FirstName", random_data_deserialized.results[0].name.first);
                phone_verification_request.AddQueryParameter("LastName", random_data_deserialized.results[0].name.last);
                phone_verification_request.AddQueryParameter("EmailAddress", random_data_deserialized.results[0].email);
                phone_verification_request.AddQueryParameter("PhoneNumber", scenarioContext["phoneno"].ToString());
                phone_verification_request.AddQueryParameter("Password", Constants.DEFAULT_PASSWORD);
                phone_verification_request.AddQueryParameter("HeroSocietyLoginType", "");
                phone_verification_request.AddQueryParameter("HeroSocietyLoginUserId", "");
                phone_verification_request.AddQueryParameter("DeviceType", Constants.DEFAULT_ANDROID_VERSION);

                scenarioContext["FirstName"] = random_data_deserialized.results[0].name.first;
                scenarioContext["LastName"] = random_data_deserialized.results[0].name.last;
                scenarioContext["FullName"] = scenarioContext["FirstName"] + " " + scenarioContext["LastName"];
                scenarioContext["EmailAddress"] = random_data_deserialized.results[0].email;

                //----------------------------Execution-------------------------------------//

                var phone_verfication_response = client.Execute(phone_verification_request);
                scenarioContext["APIResponse"] = phone_verfication_response.Content;
                Assert.IsTrue(phone_verfication_response.StatusCode == System.Net.HttpStatusCode.OK, "Error Response Received");
                var phone_verfication_responsedata = JsonConvert.DeserializeObject<AuthenticationResponse>(phone_verfication_response.Content);
                Assert.IsNotNull(phone_verfication_responsedata);
                Assert.IsTrue(phone_verfication_responsedata.success);
                result = phone_verfication_responsedata.result.accessToken;
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                goto retryRandomData;
            }
            return result;
        }

        /// <summary>
        /// Retrieve the superhero's name from the Kare Club.
        /// </summary>
        /// <param name="bearerToken">fileToken that needs to be upload files</param>
        /// <returns>Get Super Hero name as String</returns>
        public String GetHasSuperHero(String bearerToken)
        {
            //-----------------Has Super Hero Request------------------------------//
            var get_has_super_hero = new RestRequest(Endpoints.GET_HAS_SUPER_HERO, Method.Get);
            get_has_super_hero.AddHeader("Authorization", $"Bearer {bearerToken}");


            //----------------------------Execution-------------------------------------//

            var response = client.Execute(get_has_super_hero);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetHasSuperHeroResponse>(response.Content);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsNotNull(responseData);
            return responseData.result.name;
        }

        /// <summary>
        /// When the sidekick successfully becomes a hero, the hero of the sidekick status becomes a superhero.
        /// </summary>
        /// <param name="bearerToken">fileToken that needs to be upload files</param>
        public void BecomeSuperHero(String bearerToken)
        {
            //-----------------Become Super Hero Request------------------------------//
            var become_super_hero_request = new RestRequest(Endpoints.BECOME_SUPER_HERO, Method.Post);
            become_super_hero_request.AddHeader("Authorization", $"Bearer {bearerToken}");


            //----------------------------Execution-------------------------------------//

            var become_super_hero_response = client.Execute(become_super_hero_request);

            Assert.IsTrue(become_super_hero_response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(become_super_hero_response.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);

        }

        /// <summary>
        /// Retrieve the overall earnings of the Kare Club.
        /// </summary>
        /// <param name="bearerToken">fileToken that needs to be upload files</param>
        /// <param name="amount">Store amount as Int</param>
        public void GetClubEarnMoney(String bearerToken, int amount)
        {
            //-----------------Club Info Request------------------------------//
            var get_club_info = new RestRequest(Endpoints.GET_CLUB_INFO, Method.Get);
            get_club_info.AddHeader("Authorization", $"Bearer {bearerToken}");


            //----------------------------Execution-------------------------------------//

            var response = client.Execute(get_club_info);

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var responseData = JsonConvert.DeserializeObject<GetClubInfoResponse>(response.Content);
            scenarioContext["APIResponse"] = response.Content;
            Assert.IsNotNull(responseData);
            List<int> earnMoneyAmounts = new List<int>();
            for (int i = 0; i < responseData.result.sidekicksPerformance.Length; i++)
            {
                Sidekicksperformance sidekick = responseData.result.sidekicksPerformance[i];
                var totalEarnedMoney = sidekick.totalEarned;
                int money = Convert.ToInt32(totalEarnedMoney);
                earnMoneyAmounts.Add(money);
            }
            int earnMoney = 0;
            foreach (int earnMoneyAmount in earnMoneyAmounts)
            {
                earnMoney += earnMoneyAmount;
            }
            var earned = responseData.result.earned;
            if (amount == 0)
            {
                Assert.IsTrue(earnMoney.Equals(0));
                Assert.IsTrue(earned.Equals(earnMoney));
            }
            else
            {
                Assert.IsTrue(earned.Equals(earnMoney));
            }



        }

        /// <summary>
        /// Update GCHEXS Information
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        public void UpdateDemographicInformationV2(String bearerToken)
        {
            //-----------------Update Demographic Information Request------------------------------//
            var update_demographic_request = new RestRequest(Endpoints.UPDATE_DEMOGRAPHIC_INFORMATION_V2, Method.Post);
            update_demographic_request.AddHeader("Authorization", $"Bearer {bearerToken}");


            var requestbody = new
            {
                race = "Asian or Pacific Islander",
                gender = "Male",
                eyeColor = "Black", 
                hairColor = "Black", 
                placeOfBirthId = 1,
                height = "5,7",
                weight = "120",
                selectedStateIds = new int[] { 48 },
                stateId = 21
 
            };

            update_demographic_request.RequestFormat = DataFormat.Json;
            update_demographic_request.AddJsonBody(requestbody);

            //----------------------------Execution-------------------------------------//

            var update_demographic_reponse = client.Execute(update_demographic_request);

            Assert.IsTrue(update_demographic_reponse.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(update_demographic_reponse.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }


        /// <summary>
        /// Finger print confirmation for Georgia state
        /// </summary>
        /// <param name="bearerToken">Get the bearer token from the Authenticate API</param>
        public void FingerPrintConfirmation(String bearerToken)
        {
            //-----------------FingerPrintConfirmation Request------------------------------//
            var finger_print_request = new RestRequest(Endpoints.FINGER_PRINT_CONFIRM, Method.Post);
            finger_print_request.AddHeader("Authorization", $"Bearer {bearerToken}");



            //----------------------------Execution-------------------------------------//

            var finger_print_reponse = client.Execute(finger_print_request);

            Assert.IsTrue(finger_print_reponse.StatusCode == System.Net.HttpStatusCode.OK, Constants.API_ERROR_RESPONSE);
            var response = JsonConvert.DeserializeObject<GeneralResponse>(finger_print_reponse.Content);
            Assert.IsTrue(response.success);
            Assert.IsTrue(response.__abp);
        }


        #endregion

    }
}
