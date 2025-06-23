using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Data.API
{
    public static class Endpoints
    {
        public const string VERIFY_ACCOUNT = "/api/TokenAuth/VerificationAccountV1?";
        public const string UPLOAD_PHOTO = "/api/File/UploadHeroPhoto";
        public const string PHONE_VERIFICATION = "/api/TokenAuth/VerificationPhoneCode?";
        public const string REGISTER_HERO = "/api/TokenAuth/HeroRegisterV1?";
        public const string AUTH = "/api/TokenAuth/Authenticate";
        public const string DELETE_HERO_ACCOUNT = "/api/services/app/FrontendProfile/DeleteHeroAccount";
        public const string SEND_SMS = "/api/services/app/FrontendHeroRegister/SendSmsCodeReturnKey?";
        public const string GET_MESSAGES_BY_CHAT = "/api/services/app/FrontendDialog/GetMessages";
        public const string SEND_MESSAGE_BY_CHAT = "/api/services/app/FrontendDialog/SendMessage";
        public const string GET_CHATS = "/api/services/app/FrontendDialog/GetAllDialog";
        public const string SAVE_PERSONAL_INFO = "/api/services/app/FrontendHeroRegister/SavePersonalInfo";
        public const string CREATE_HERO_LICENSE = "/api/services/app/FrontendHeroRegister/CreateHeroLicenseInfoV2";
        public const string GET_MISSING_DOCUMENTS = "/api/services/app/FrontendHeroRegister/GetDocumentsV2?IsShowStaticDoc=false";
        public const string UPLOAD_RESUME_FILE = "/api/File/UploadMutilHeroResumeDB";
        public const string RESUME_UPLOADED = "/api/services/app/FrontendHeroRegister/GetResumeIds?";
        public const string UPDATE_HERO_BANK_ACCOUNT = "/api/services/app/FrontendHeroRegister/UpdateHeroBankAccountAsync";
        public const string UPLOAD_DOCUMENT = "/api/File/UploadHeroFile";
        public const string SAVE_DOCUMENT = "/api/services/app/FrontendHeroRegister/SaveHeroDocumentToDb";
        public const string TERMS_AND_SERVICE = "/api/services/app/FrontendHeroRegister/GetTermAndService?";
        public const string ACCEPT_TERMS_OF_SERVICE = "/api/services/app/FrontendHeroRegister/AcceptTermOfService";
        public const string SUBMIT_DOCUMENTS = "/api/services/app/FrontendHeroRegister/DocumentSubmitForReview";
        public const string UPLOAD_RESUME = "/api/File/UploadMutilHeroResumeDB";
        public const string SUBMIT_RESUME = "/api/services/app/FrontendHeroRegister/CompleteUploadResume";
        public const string REVIEW_TAX_FORM = "/api/services/app/FrontendHeroRegister/PreviewPdf";
        public const string SIGN_TAX_FORM = "/api/services/app/FrontendHeroRegister/SignPdfV2";
        public const string GET_PERSONAL_INFO = "/api/services/app/FrontendProfile/GetPersonInfo";
        public const string GET_HERO_MARKETS = "/api/services/app/FrontendHeroRegister/GetHeroMarkets";
        public const string UPDATE_HERO_MARKETS = "/api/services/app/FrontendHeroRegister/UpdateHeroMarketPatch";
        public const string COMPLETE_HERO_REGISTER = "/api/services/app/FrontendHeroRegister/CompleteRegister";
        public const string GET_POSTED_SHIFT = "/api/services/app/FrontendShift/GetAllShiftsV2";
        public const string GET_SHIFT_DETAILS = "/api/services/app/FrontendShift/GetShiftDetailById";
        public const string APPLY_SHIFT = "/api/services/app/FrontendShift/PostShift?";
        public const string IS_HERO_WORKING_SHIFT = "/api/services/app/FrontendShift/IsHaveWorkingShift";
        public const string CONFIRM_SHIFT = "/api/services/app/FrontendShift/MyShiftConfirm";
        public const string READY_TO_GO_SHIFT = "/api/services/app/FrontendShift/MyShiftReadyToGo";
        public const string CHECK_IN_SHIFT = "/api/services/app/FrontendShift/MyShiftCheckIn";
        public const string UPDATE_HERO_LOCATION = "/api/services/app/FrontendShift/UploadLocation";
        public const string GET_SHIFT_DISTANCE = "/api/services/app/FrontendShift/GetDistanceByShiftId";
        public const string GET_SHIFT_CHECKOUT_INFO = "/api/services/app/FrontendShift/GetMyShiftCheckOutInfo";
        public const string CHECKOUT_SUBMIT_REVIEW = "/api/services/app/FrontendShift/checkoutAndSubmitHeroReviews";
        public const string PREVIEW_BID_FORM = "/api/services/app/FrontendHeroRegister/PreviewBackgroundInfoDisclosureForm";
        public const string SIGN_BID_FORMV2 = "/api/services/app/FrontendHeroRegister/SignBackgroundInfoDisclosureFormV2";
        public const string UPDATE_LIVE_IN_THE_STATE = "api/services/app/FrontendHeroRegister/UpdateLiveInTheStateStatus";
        public const string PREVIEW_HEALTH_STATUS_PHYSICALEXAM_FORM = "/api/services/app/FrontendHeroRegister/PreviewHealthStatusPhysicalExam";
        public const string SIGN_HEALTH_STATUS_PHYSICALEXAM_FORM = "/api/services/app/FrontendHeroRegister/SignHealthStatusPhysicalExam";
        public const string SUBMIT_HERO_REFERENCE1 = "/api/services/app/FrontendHeroRegister/SubmitHeroReferencesV2";
        public const string SUBMIT_HERO_REFERENCE2 = "/api/services/app/FrontendHeroRegister/SubmitHeroReferencesV3";
        public const string PREVIEW_GCHEX = "/api/services/app/FrontendHeroRegister/PreviewGchexPdf";
        public const string SIGN_GCHEX = "/api/services/app/FrontendHeroRegister/SignGchexsPdf";
        public const string GET_LAST_ONBOARDING_STATE = "/api/services/app/FrontendProfile/GetLastOnboardingState";
        public const string GET_CAN_REISTER_ANOTHER_STATE = "/api/services/app/FrontendHeroRegister/CanRegisterAnotherState";
        public const string GET_HERO_ONBOARDING_STATE_AND_LICENSElIST = "/api/services/app/FrontendHeroLicenseStatus/GetHeroOnboardingStateAndLicenseList";
        public const string GET_HERO_ONBOARDING_STATUS_LIST = "/api/services/app/FrontEndHeroLicenseStatus/GetHeroOnboardingStatusList?facilityType=license";
        public const string GET_INVITE_ID = "/api/services/app/FrontendInvite/GetInviteIds";
        public const string SEND_INVITE = "/api/services/app/FrontendInvite/Send";
        public const string SANTIZE_INVITE_PHONENUMBER = "/api/services/app/FrontendInvite/SantizeInvitePhoneNumber";
        public const string UPDATE_STATUS = "/api/services/app/FrontendInvite/UpdateStatus";
        public const string UPDATE_STATUS_V2 = "/api/services/app/FrontendInvite/UpdateStatus_v2";
        public const string GET_CLUB_INFO = "/api/services/app/FrontendClub/GetClubInfo";
        public const string GET_HAS_SUPER_HERO = "/api/services/app/FrontendClub/HasSuperhero";
        public const string BECOME_SUPER_HERO = "/api/services/app/FrontendClub/BecomeSuperhero";
        public const string UPDATE_DEMOGRAPHIC_INFORMATION_V2 = "/api/services/app/FrontendHeroRegister/UpdateDemographicInformationV2";
        public const string FINGER_PRINT_CONFIRM = "/api/services/app/FrontendHeroRegister/FingerPrintConfirm";



        public const string EXTERNAL_URL = "https://api.parser.name/?api_key=31e6e137db191f263892b38987973a73f&endpoint=generate";
        public const string RANDOM_DATA_URL = "https://randomuser.me/api/?results=3&nat=us";
    }
}
