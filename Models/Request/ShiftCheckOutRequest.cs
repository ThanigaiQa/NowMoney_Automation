using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Models.Request
{

    public class ShiftCheckOutRequest
    {
        public string reviewComment { get; set; }
        public string shiftId { get; set; }
        public Shiftrating[] shiftRatings { get; set; }
        public DateTime checkOutTime { get; set; }


        public class Shiftrating
        {
            public string reviewType { get; set; }
            public string rating { get; set; }
        }
    }

}
