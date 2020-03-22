using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19
{
    public class JohnsHopkinsDataOptions
    {
        public JohnsHopkinsDataOptions()
        {
            DailyReportsDir = "csse_covid_19_data\\csse_covid_19_daily_reports";
        }

        public string RootDir { get; set; }

        public string DailyReportsDir { get; set; }

        public string DailyReportsFullPath => Path.Join(RootDir, DailyReportsDir);
    }
}
