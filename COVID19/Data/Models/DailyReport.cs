using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19.Data.Models
{
    public class DailyReport
    {
        [Name("Province/State")]
        public string ProvinceState { get; set; }

        [Name("Country/Region")]
        public string CountryRegion { get; set; }

        [Name("Last Update")]
        public DateTime? LastUpdate { get; set; }

        public int? Confirmed { get; set; }

        public int? Deaths { get; set; }

        public int? Recovered { get; set; }

        [Optional]
        public decimal? Latitude { get; set; }

        [Optional]
        public decimal? Longitude { get; set; }
    }
}
