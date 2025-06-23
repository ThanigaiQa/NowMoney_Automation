using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Models.Request
{
    public class HeroReference
    {
        public int stateId { get; set; }
        public Heroreference[] heroReferences { get; set; }

        public class Heroreference
        {
            public string name { get; set; }
            public string emailOrPhone { get; set; }
            public string relationship { get; set; }

        }
    }
}






