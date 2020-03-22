using COVID19.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace COVID19.Tests
{
    public class Tests
    {
        private JohnsHopkinsDataOptions _options;
        
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.nunit.json")
               .Build();
            _options = config.GetSection("JohnsHopkinsData").Get<JohnsHopkinsDataOptions>();
        }

        [Test]
        public void CheckForNewCsvFilesShouldReturnNewFiles()
        {
            // Arrange 
            var options = new Mock<IOptionsMonitor<JohnsHopkinsDataOptions>>();
            options.Setup(opt => opt.CurrentValue).Returns(_options);
            var service = new DailyReportService(options.Object);

            // Act 
            service.CheckForNewDailyReports();

            // Assert 
            Assert.IsTrue(service.DailyReports.Count > 0);
        }

        [Test]
        public void CheckForNewCsvFilesShouldReturnNoNewFilesOnSecondRun()
        {
            // Arrange
            var options = new Mock<IOptionsMonitor<JohnsHopkinsDataOptions>>();
            options.Setup(opt => opt.CurrentValue).Returns(_options);
            var service = new DailyReportService(options.Object);

            // Act 
            service.CheckForNewDailyReports();
            int before = service.DailyReports.Count;

            service.CheckForNewDailyReports();
            int after = service.DailyReports.Count;

            // Assert
            Assert.IsTrue(before > 0, "No files found, test invalid");
            Assert.AreEqual(before, after);
        }
    }
}