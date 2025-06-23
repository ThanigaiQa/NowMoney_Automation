using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Models.Response
{
    public class GetHeroOnboardingStateAndLicenseList
    {

            public Result result { get; set; }
            public object targetUrl { get; set; }
            public bool success { get; set; }
            public object error { get; set; }
            public bool unAuthorizedRequest { get; set; }
            public bool __abp { get; set; }
        

        public class Result
        {
            public Onboardinglist[] onboardingList { get; set; }
        }

        public class Onboardinglist
        {
            public string stateName { get; set; }
            public int stateId { get; set; }
            public string licenseTypeName { get; set; }
            public int onboardingStatus { get; set; }
            public string facilityType { get; set; }
            public bool hasRejectedDocument { get; set; }
        }
    
    }
}
