using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Data.API
{

    
    public static class Constants
    {
        #region Kare
        public const string IMAGE_PATH = "\\Data\\API\\Images\\KARE.jpeg";
        public const string DEFAULT_PASSWORD = "123qwe";
        public const string APPVERSION = "5";
        public const string DEFAULT_PHONECODE = "111111";
        public const string DEFAULT_ANDROID_VERSION = "Android31(1.5.19)";
        public const string BIRTHDAY = "04/25/1990";
        public const string ADDRESS = "99 Zapata Street";
        public const string CITY = "Zapata";
        public const string ZIPCODE = "78076";
        public const string BANKNAME = "Automation Test";
        public const string DEFAULT_COMMUNITYNAME = "Mezcal Community";
        public const string CST_TIME = "Central Standard Time";
        public const string ISO8601_TIMEFORMAT = "yyyy-MM-ddTHH:mm:ss.fffZ";
        public const string SIGNPICBASE64 = "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xBgAYmIMADExBoCYGANATIwBICbGABATYwCIiTEAxMQYAGJiDAAxMQaAmBgDQEyMASAmxgAQE2MAiIkxAMTEGABiYgwAMTEGgJgYA0BMjAEgJsYAEBNjAIiJMQDExBgAYmIMADExBoCYGANATIwBICbGABATYwCIiTEAxMQYAGJiDAAxMQaAmBgDQEyMASAmxgAQE2MAiIkxAMTEGABiYgwAMTEGgJgYA0BMjAEgJsYAEBNjAIiJMQDExBgAYmIMADExBoCYGANATIwBICbGABATYwCIiTEAxMQYAGJiDAAxMQaAmBgDQEyMASAmxgAQE2MAiIkxAMTEGABiYgwAMTEGgJgYA0BMjAEgJsYAEBNjAIiJMQDExBgAYmIMADExBoCYGANATIwBICbGABATYwCIiTEAxMQYAGJiDAAxMQaAmBgDQEyMASAmxgAQE2MAiIkxAMTEGABiYgwAMTEGgJgYA0BMjAEgJsYAEBNjAIiJMQDExBgAYmIMADExBoCYGANATIwBICbGABATYwCIiTEAxMQYAGJiDAAxMQaAmBgDQEyMASAmxgAQE2MAiIkxAMTEGABiYgwAMTEGgJgYA0BMjAEgJsYAEBNjAIiJMQDExBgAYmIMADExBoCYGANATIwBICbGABATYwCIiTEAxMQYAGJiDAAxMQaAmBgDQEyMASAmxgAQE2MAiIkxAMTEGABiYgwAMTEGgJgYA0Dqx4//AeG8YwUYJZVCAAAAAElFTkSuQmCC";
        public const string ADDRESS_NJ = "32 Branford Pl #2723";
        public const string CITY_NJ = "Newark";
        public const string ZIPCODE_NJ = "07102";
        public const string MEDICALCONDITIONS_INDIANA = "29";
        public const string TESTNAME = "Test";
        public const string HEALTHSTATUS_HEIGHT = "158";
        public const string HEALTHSTATUS_WEIGHT = "70";
        public const string HEROREFERENCE_NAME = "John";
        public const string HEROREFERENCE_EMAIL = "john@email.com";
        public const string HEROREFERENCE_RELATION = "Son";
        public const string WEB_IMAGE_PATH = "Data\\API\\Images";
        public const string WEB_IMAGE_NAME = "KARE.jpeg";
        public const string WEB_PDF_PATH = "Data\\WEB";
        public const string WEB_PDF_NAME = "Kare.pdf";
        public const string HERO_STATUS = "3";
        public const string IN_PIPELINE_STATUS = "2";
        public const string REFERRAL_MADE_STATUS = "1";
        public const string API_ERROR_RESPONSE = "Error Response Received";
        public const string TEST_MESSAGE = "Automation";

        public static readonly Dictionary<string, string> STATE_ID = new Dictionary<string, string>
        {
            { "ALABAMA", "9" },
            { "TEXAS", "43" },
            { "WYOMING", "14" },
            { "ALASKA", "45" },
            { "ARIZONA", "15" },
            { "GEORGIA", "21" },
            { "WISCONSIN", "41" },
            { "PENNSYLVANIA", "32" },
            { "INDIANA", "51" },
            {  "NEWJERSEY", "46" }
        };

        public static readonly Dictionary<string, string> LICENSE_TYPE = new Dictionary<string, string>
        {
            { "CNA", "2" },
            { "CMA", "8" },
            { "CCHT", "64" },
            { "AL_Caregiver_HHA_CMA_AL_GNA", "4" },
            { "LVN_LVP", "16" },
            { "LVN_LVP_DIALISYS", "128" },
            { "RN", "32" },
            { "RN_DIALISYS", "256" }
        };

        public static readonly Dictionary<string, string> BANK_ACCOUNT_TYPE = new Dictionary<string, string>
        {
            { "CHECKING", "20" },
            { "SAVING", "10" }
        };

        #endregion

        #region NowMoney

        public static readonly List<string> showAllFilterValues = new List<string>
        {
            "Show All",
            "Account Active",
            "Registration started",
            "Details submitted",
            "Registration successful",
            "Registration failed",
            "Card ready for activation"
        };

        public static readonly List<string> displayFilterValues = new List<string>
        {
            "First name",
            "Last name",
            "Emirates ID",
            "Account status",
            "IBAN",
            "Reference ID",
            "Designation",
            "Department",
            "Account active since",
            "Actions"
        };

        public static readonly List<string> authorizeBulkDisplayFilterValues = new List<string>
        {
            "Payment ID",
            "File Name",
            "Amount",
            "Fees",
            "Status",
            "Created at",
            "Updated at",
            "Scheduled For",
            "Download",
            "Approve",
            "Reject"
        };

        public static readonly List<string> authorizeIndividualDisplayFilterValues = new List<string>
        {
            "Emirates ID",
            "Type",
            "Employee name",
            "Amount",
            "Fees",
            "Description",
            "Status",
            "Created at",
            "Updated at",
            "Scheduled For",
            "Download",
            "Approve",
            "Reject"
        };

        public const string endpoint_Dashboard = "dashboard";
        public const string endpoint_Employees = "employees";
        public const string endpoint_TransactionStatus = "statusReport";
        public const string endpoint_LoadCorporateAccount = "uploadTransferProof";
        public const string endpoint_Payments = "viewPayment";
        public const string endpoint_WpsPayments = "wpsStatus";
        public const string endpoint_Users = "users";
        public const string endpoint_Permissions = "permissions";
        public const string endpoint_Login = "_login";
        public const string endpoint_ForgotPassword = "_forgot-password";
        public const string endpoint_BookADemo = "book-a-demo";
        public const string endpoint_CreatePayments_NonWPSPaymentsBulk = "createBulkDisbursement";
        public const string endpoint_CreatePayments_NonWPSPaymentsIndividual = "createIndividualDisbursement";
        public const string endpoint_CreatePayments_WPSPayment = "createWPSBulkDisbursement";
        public const string endpoint_AuthorizePayments_NonWPSPaymentsBulk = "authorizeBulkPayment";
        public const string endpoint_AuthorizePayments_NonWPSPaymentsIndividual = "authorizeIndividualPayment";
        public const string endpoint_AuthorizePayments_WPSPayment = "authorizeWpsPayment";

        #endregion

    }
}
