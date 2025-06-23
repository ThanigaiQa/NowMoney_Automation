using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Models.Response
{
    public class GetHasSuperHeroResponse
    {

            public Result result { get; set; }
            public object targetUrl { get; set; }
            public bool success { get; set; }
            public object error { get; set; }
            public bool unAuthorizedRequest { get; set; }
            public bool __abp { get; set; }
        

        public class Result
        {
            public bool hasSuperhero { get; set; }
            public string name { get; set; }
        }

    }
}
