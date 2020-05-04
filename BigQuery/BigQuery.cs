using CloudRun_DotNetCore.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2.Data;
using Google.Cloud.BigQuery.V2;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CloudRun_DotNetCore.BigQuery
{
    public class BigQuery : IBigQuery
    {

        const string PROJECT_ID = "YOUR PROJECT ID";
       
        public BigQueryClient GetBigqueryClient()
        {
           string jsonpath = "NAME OF YOUR CREDENTIAL JSON FILE";

            var credential = GoogleCredential.FromFile(jsonpath);
            using (var jsonStream = new FileStream(jsonpath, FileMode.Open, FileAccess.Read, FileShare.Read))
                credential = GoogleCredential.FromStream(jsonStream);

            return BigQueryClient.Create(PROJECT_ID, credential);
        }

     
        public List<Covid> get_data(int queryid, string qryparams)
        {
            List<Covid> covids = new List<Covid>();
            var client = GetBigqueryClient();
            var table = client.GetTable("bigquery-public-data", "covid19_ecdc","covid_19_geographic_distribution_worldwide");
            var sql = "";
            BigQueryParameter[] parameters = null;
            if (queryid == 1)
            {
                sql = $"SELECT * FROM {table}";
            } else if (queryid == 2)
            {
                sql = $"SELECT * FROM {table} where date = @date";
                parameters = new[]
                    {
                        new BigQueryParameter("date", BigQueryDbType.Date, qryparams),
                        
                    };
            } else
            {
                sql = $"SELECT * FROM {table} where geo_id = @geoid";
                parameters = new[]
                    {
                        new BigQueryParameter("geoid", BigQueryDbType.String, qryparams),

                    };
            }

            
            var results = client.ExecuteQuery(sql, parameters);

            foreach (BigQueryRow row in results)
            {
                Covid s = new Covid();
                s.Date = Convert.ToString(row["date"]);
                s.day = Convert.ToInt32(row["day"]);
                s.month = Convert.ToInt32(row["month"]);
                s.year = Convert.ToInt32(row["year"]);
                s.daily_confirmed_cases = Convert.ToInt32(row["daily_confirmed_cases"]);
                s.daily_deaths = Convert.ToInt32(row["daily_deaths"]);
                s.confirmed_cases = Convert.ToInt32(row["confirmed_cases"]);
                s.deaths = Convert.ToInt32(row["deaths"]);
                s.countries_and_territories = Convert.ToString(row["countries_and_territories"]);
                s.geo_id = Convert.ToString(row["geo_id"]);
                s.country_territory_code = Convert.ToString(row["country_territory_code"]);
                s.pop_data_2018 = Convert.ToInt32(row["pop_data_2018"]);
                covids.Add(s);
               
            }
            return covids;
        }
        public List<Covid> get_countries()
        {
            List<Covid> covids = get_data(1,"");
            return covids;
        }
        public List<Covid> get_country_details(string country_code)
        {
            List<Covid> covids = get_data(3, country_code);
            return covids;
        }

        public List<Covid> get_day(string date_id)
        {
            List<Covid> covids = get_data(2,date_id);
            return covids;
        }
    }
}
