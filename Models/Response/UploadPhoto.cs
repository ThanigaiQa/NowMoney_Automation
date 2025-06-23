using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.PageObjects.API.Models.Response
{
    public class UploadPhoto
    {
        public Result result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }

        public class Result
        {
            public string fileToken { get; set; }
            public string fileName { get; set; }
            public string fileType { get; set; }
            public int code { get; set; }
            public object message { get; set; }
            public object details { get; set; }
            public object validationErrors { get; set; }
        }
    }
}
