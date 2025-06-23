using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.PageObjects.API.Models.Request
{
    public class SaveDocumentRequest
    {
        public Filelist[] fileList { get; set; }
        public string expiredDate { get; set; }
        public int documentTypeId { get; set; }
        public bool IsRegistered { get; set; }

        public int stateId { get; set; }

        public class Filelist
        {
            public string fileToken { get; set; }
            public string fileName { get; set; }
            public string fileType { get; set; }
            public int code { get; set; }
            public object message { get; set; }
            public object details { get; set; }
            public object validationErrors { get; set; }
            public bool newUp { get; set; }
        }
    }

}
