using CloudRun_DotNetCore.Models;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudRun_DotNetCore.BigQuery
{
    public interface IBigQuery
    {
        public List<Covid> get_countries();
        public List<Covid> get_day(string date_id);
        public List<Covid> get_country_details(string country_code);
        BigQueryClient GetBigqueryClient();
    }
}
