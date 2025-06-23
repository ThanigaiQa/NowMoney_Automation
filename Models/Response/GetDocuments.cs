using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.PageObjects.API.Models.Response
{

    public class GetDocuments
    {
        public Result result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }


        public class Result
        {
            public int heroId { get; set; }
            public int userId { get; set; }
            public int licenseType { get; set; }
            public object subLicenseType { get; set; }
            public string licenseNumber { get; set; }
            public string licenseDescription { get; set; }
            public Documenttype[] documentTypes { get; set; }
        }

        public class Documenttype
        {
            public int id { get; set; }
            public string typeName { get; set; }
            public object description { get; set; }
            public bool isNeed { get; set; }
            public bool neesExpiredDate { get; set; }
            public bool isOptional { get; set; }
            public object[] documents { get; set; }
            public bool isStaticDocument { get; set; }
            public object staticDocumentType { get; set; }
            public object expirationTime { get; set; }
            public bool enableGroup { get; set; }
            public bool needsExpirationDate { get; set; }
            public bool isRejected { get; set; }
            public object rejectFromPipe { get; set; }
        }
    }

}
