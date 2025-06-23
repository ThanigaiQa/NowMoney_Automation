using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Models.Response
{

    public class GetPersonInfoResponse
    {
        public Result result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }

        public class Result
        {
            public int userId { get; set; }
            public string userName { get; set; }
            public string emailAddress { get; set; }
            public string name { get; set; }
            public string firstName { get; set; }
            public string surname { get; set; }
            public string phoneNumber { get; set; }
            public string ssn { get; set; }
            public DateTime birthDate { get; set; }
            public object currentAddressStartDate { get; set; }
            public string profilePictureId { get; set; }
            public string shiftRating { get; set; }
            public string reliabilityScore { get; set; }
            public string averageReliabilityScore { get; set; }
            public bool isLicenseHero { get; set; }
            public int registerStep { get; set; }
            public int heroRegisterStatus { get; set; }
            public int resumeType { get; set; }
            public bool isInterviewPass { get; set; }
            public bool hadBeenHero { get; set; }
            public int licenseType { get; set; }
            public int stateId { get; set; }
            public object kareClubIsEnabled { get; set; }
            public object[] onboardedStateIds { get; set; }
            public object[] disqualifiedStateIds { get; set; }
            public bool hasVaccine { get; set; }
            public object[] documentsAboutToExpire { get; set; }
            public bool hasCOVIDExemption { get; set; }
            public bool idBadgeCreated { get; set; }
            public bool taxInfoStatus { get; set; }
            public bool isHero { get; set; }
            public string primaryMarketName { get; set; }
            public string licenseTypeNames { get; set; }
            public string heroStatus { get; set; }
            public bool isDisqualifyFromOhioWebCheck { get; set; }
            public bool hasRejectedDocument { get; set; }
        }
    }

}
