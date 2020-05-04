using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudRun_DotNetCore.BigQuery;
using CloudRun_DotNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudRun_DotNetCore.Controllers
{
    [ApiController]
    [Route("country")]
   
    public class BigQueryController : ControllerBase
    {
        private readonly IBigQuery _bigQuery;

        public BigQueryController(IBigQuery bigQuery)
        {
            _bigQuery = bigQuery;
        }
        [HttpGet]
        [HttpGet("all")]
        public IEnumerable<Covid> Countries()
        {
            List<Covid> covids = _bigQuery.get_countries();
            return covids.ToArray();

        }
        [HttpGet("day/{date}")] 
        public IEnumerable<Covid> day(string date)
        {
            List<Covid> covids = _bigQuery.get_day(date);
            return covids.ToArray();
        }
        [HttpGet("code/{countrycode}")]
        public IEnumerable<Covid> country(string countrycode)
        {
            List<Covid> covids = _bigQuery.get_country_details(countrycode);
            return covids.ToArray();
        }
    }
}