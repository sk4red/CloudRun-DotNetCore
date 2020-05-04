using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudRun_DotNetCore.Models
{
    public class Covid
    {
        public string Date { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int daily_confirmed_cases { get; set; }
        public int daily_deaths { get; set; }
        public int confirmed_cases { get; set; }
        public int deaths { get; set; }
        public string countries_and_territories { get; set; }
        public string geo_id { get; set; }
        public string country_territory_code { get; set; }
        public int pop_data_2018 { get; set; }


    }
}
