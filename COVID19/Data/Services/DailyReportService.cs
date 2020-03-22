using COVID19.Data.Models;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace COVID19.Data.Services
{
    public class DailyReportService
    {
        private readonly IOptionsMonitor<JohnsHopkinsDataOptions> _options;
        private List<string> _processedFiles = new List<string>();
        
        public DailyReportService(
            IOptionsMonitor<JohnsHopkinsDataOptions> options)
        {
            _options = options;
        }

        public List<DailyReport> DailyReports { get; }  = new List<DailyReport>();

        public void CheckForNewDailyReports()
        {
            var sourceDirectoryPath = _options.CurrentValue.DailyReportsFullPath;
            var allFiles = Directory.GetFiles(sourceDirectoryPath, "*.csv");
            var newFiles = allFiles.Except(_processedFiles).ToList();
            _processedFiles.AddRange(newFiles);
            var newRecords = newFiles.SelectMany(path => ReadFile(path));
            DailyReports.AddRange(newRecords);
        }

        private IEnumerable<DailyReport> ReadFile(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<DailyReport>().ToList();
                return records;
            }
        }
    }
}
