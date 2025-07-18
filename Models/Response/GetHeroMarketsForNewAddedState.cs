﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KARE.E2E.AUTOMATION.Models.Response
{
    public class GetHeroMarketsForNewAddedState
    {

            public Result result { get; set; }
            public object targetUrl { get; set; }
            public bool success { get; set; }
            public object error { get; set; }
            public bool unAuthorizedRequest { get; set; }
            public bool __abp { get; set; }
        

        public class Result
        {
            public Onboardmarketgroup[] onboardMarketGroups { get; set; }
            public Othermarketgroup[] otherMarketGroups { get; set; }
            public int[] selectedMarketIds { get; set; }
            public int primaryMarketId { get; set; }
        }

        public class Onboardmarketgroup
        {
            public int stateId { get; set; }
            public string state { get; set; }
            public Market[] markets { get; set; }
        }

        public class Market
        {
            public int marketId { get; set; }
            public string marketName { get; set; }
            public bool isSelected { get; set; }
        }

        public class Othermarketgroup
        {
            public int stateId { get; set; }
            public string state { get; set; }
            public Market1[] markets { get; set; }
        }

        public class Market1
        {
            public int marketId { get; set; }
            public string marketName { get; set; }
            public bool isSelected { get; set; }
        }

    }
}
