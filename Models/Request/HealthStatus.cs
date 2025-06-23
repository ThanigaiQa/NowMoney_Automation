using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Models.Request
{
    public class HealthStatus
    {
        public string height { get; set; }
        public string weight { get; set; }
        public bool historyOfSeriousHealthIssue { get; set; }
        public string historyOfSeriousHealthIssueDescription { get; set; }
        public bool currentIssueToJobDuties { get; set; }
        public string currentIssueToJobDutiesDescription { get; set; }
        public string[] selectedMedicalConditions { get; set; }
        public bool otherMedicalConditionChecked { get; set; }
        public string otherMedicalConditionDescription { get; set; }
        public Medicationnamereason[] medicationNameReasons { get; set; }

        public class Medicationnamereason
        {
            public string medicationName { get; set; }
            public string medicationReason { get; set; }
        }
    }
}


