using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.PageObjects.API.Models.Response
{

    public class RandomData
    {
        public int results { get; set; }
        public object error { get; set; }
        public Datum[] data { get; set; }

        public class Datum
        {
            public Salutation salutation { get; set; }
            public object title { get; set; }
            public Name name { get; set; }
            public string email { get; set; }
            public Country country { get; set; }
        }

        public class Salutation
        {
            public string salutation { get; set; }
            public string initials { get; set; }
            public string lastname { get; set; }
        }

        public class Name
        {
            public object nickname { get; set; }
            public Firstname firstname { get; set; }
            public object[] middlenames { get; set; }
            public Lastname lastname { get; set; }
        }

        public class Firstname
        {
            public string name { get; set; }
            public string name_ascii { get; set; }
            public bool validated { get; set; }
            public string gender { get; set; }
            public string gender_formatted { get; set; }
            public bool unisex { get; set; }
            public int gender_deviation { get; set; }
            public string country_code { get; set; }
            public int country_certainty { get; set; }
            public int country_rank { get; set; }
            public int country_frequency { get; set; }
            public Alternative_Countries alternative_countries { get; set; }
        }

        public class Alternative_Countries
        {
            public int GB { get; set; }
            public int US { get; set; }
            public int IT { get; set; }
        }

        public class Lastname
        {
            public string name { get; set; }
            public string name_ascii { get; set; }
            public bool validated { get; set; }
            public string country_code { get; set; }
            public int country_certainty { get; set; }
            public int country_rank { get; set; }
            public int country_frequency { get; set; }
            public Alternative_Countries1 alternative_countries { get; set; }
            public object[] lastnames { get; set; }
        }

        public class Alternative_Countries1
        {
            public int US { get; set; }
            public int DE { get; set; }
            public int GB { get; set; }
        }

        public class Country
        {
            public string country_code { get; set; }
            public int country_certainty { get; set; }
            public string country_code_alpha { get; set; }
            public string name { get; set; }
            public string continent { get; set; }
            public string demonym { get; set; }
            public string primary_language_code { get; set; }
            public string primary_language { get; set; }
            public string currency { get; set; }
            public object[] alternative_countries { get; set; }
        }
    }

}
