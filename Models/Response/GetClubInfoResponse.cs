using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Models.Response
{
    public class GetClubInfoResponse
    {
            public Result result { get; set; }
            public object targetUrl { get; set; }
            public bool success { get; set; }
            public object error { get; set; }
            public bool unAuthorizedRequest { get; set; }
            public bool __abp { get; set; }
        

        public class Result
        {
            public float earned { get; set; }
            public float potentialEarned { get; set; }
            public int completedShifts { get; set; }
            public int shiftsToComplete { get; set; }
            public float totalEarnedToDate { get; set; }
            public bool isSuspended { get; set; }
            public Superheroandfriends superHeroAndFriends { get; set; }
            public Sidekicksperformance[] sidekicksPerformance { get; set; }
        }

        public class Superheroandfriends
        {
            public Superhero superhero { get; set; }
            public Superfriend[] superfriends { get; set; }
        }

        public class Superhero
        {
            public int userId { get; set; }
            public bool isSuperHero { get; set; }
            public string name { get; set; }
            public string urlPhoto { get; set; }
        }

        public class Superfriend
        {
            public int userId { get; set; }
            public bool isSuperHero { get; set; }
            public string name { get; set; }
            public string urlPhoto { get; set; }
        }

        public class Sidekicksperformance
        {
            public float totalEarned { get; set; }
            public object completedShifts { get; set; }
            public object isSuspended { get; set; }
            public int inviteStatus { get; set; }
            public int? userId { get; set; }
            public string name { get; set; }
            public string pictureProfileId { get; set; }
        }





    }
}
