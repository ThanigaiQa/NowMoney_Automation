using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Models.Response
{

    public class GetAllShiftsResponse
    {
        public Result result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }

        public class Result
        {
            public int totalCount { get; set; }
            public Item[] items { get; set; }
        }

        public class Item
        {
            public int id { get; set; }
            public int tenantId { get; set; }
            public int marketId { get; set; }
            public DateTime startTime { get; set; }
            public DateTime endTime { get; set; }
            public DateTime startTimeLocal { get; set; }
            public DateTime endTimeLocal { get; set; }
            public string communityName { get; set; }
            public string timeZoneShortName { get; set; }
            public int heroType { get; set; }
            public string heroRoleName { get; set; }
            public string profileId { get; set; }
            public int hourlyRate { get; set; }
            public int bonusAmount { get; set; }
            public object cancelReason { get; set; }
            public bool isPromoted { get; set; }
            public string description { get; set; }
            public int status { get; set; }
            public string statusText { get; set; }
            public object approbationTime { get; set; }
            public object confirmationTime { get; set; }
            public object cancellationTime { get; set; }
            public object checkInTime { get; set; }
            public object checkOutTime { get; set; }
            public object totalBreakMinutes { get; set; }
            public object totalMealMinutes { get; set; }
            public object totalWorkMinutes { get; set; }
            public object totalPaymentAmount { get; set; }
            public int verificationTime { get; set; }
            public object verifyResult { get; set; }
            public object reviewRating { get; set; }
            public object reviewComment { get; set; }
            public int supervisorUserId { get; set; }
            public string supervisorUserName { get; set; }
            public object approverUserId { get; set; }
            public string approverUserName { get; set; }
            public object cancelerUserId { get; set; }
            public string cancelerUserName { get; set; }
            public object verifierUserId { get; set; }
            public string verifierUserName { get; set; }
            public string creatorUserName { get; set; }
            public string lastModifierUserName { get; set; }
            public string deleterUserName { get; set; }
            public object heroUserId { get; set; }
            public string heroUserName { get; set; }
            public DateTime creationTime { get; set; }
            public bool isNew { get; set; }
            public bool isNotificationActive { get; set; }
            public bool isSample { get; set; }
            public int paymentType { get; set; }
            public object expectedSessions { get; set; }
        }
    }

}
