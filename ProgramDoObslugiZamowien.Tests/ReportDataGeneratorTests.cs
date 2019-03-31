using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramDoObslugiZamowien.Model;
using ProgramDoObslugiZamowien.UI.DataManagment;
using ProgramDoObslugiZamowien.UI.FileManagment;
using System.Collections.Generic;

namespace ProgramDoObslugiZamowien.Tests
{
    [TestClass]
    public class ReportDataGeneratorTests
    {
        List<Request> requests = FileData.GetRequestsFromFile(@"..\..\..\PlikiZDanymi\\plikJSON.json");

        [TestMethod]
        public void GetRequestsTests()
        {
            var result = ReportDataGenerator.GetRequests(requests, "1");

            Assert.AreEqual(result.Count, 3);
            result = ReportDataGenerator.GetRequests(requests, "2");

            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void SumOfRequestsTests()
        {
            var result = ReportDataGenerator.SumOfRequestsPrice(requests, "1");

            Assert.AreEqual(result, 115.5);
            result = ReportDataGenerator.SumOfRequestsPrice(requests, "2");

            Assert.AreEqual(result, 10);
        }
        [TestMethod]
        public void NoOfUniqeRequestsTests()
        {
            var result = ReportDataGenerator.NoOfUniqeRequests(requests);

            Assert.AreEqual(result, 3);
            result = ReportDataGenerator.NoOfUniqeRequests(requests, "2");

            Assert.AreEqual(result, 1);
        }
        [TestMethod]
        public void NoOfRequestGroupedByNameTests()
        {
            var result = ReportDataGenerator.NoOfRequestGroupedByName(requests);

            Assert.AreEqual(result, 3);
            result = ReportDataGenerator.NoOfRequestGroupedByName(requests, "1");

            Assert.AreEqual(result, 2);
        }
        [TestMethod]
        public void AverageRequestTests()
        {
            var result = ReportDataGenerator.AverageRequests(requests);

            Assert.AreEqual(result, 41.83);
            result = ReportDataGenerator.AverageRequests(requests, "1");

            Assert.AreEqual(result, 57.75);
        }
        [TestMethod]
        public void RequestsInRangeTests()
        {
            var result = ReportDataGenerator.RequestsInRange(requests, 40, 90);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].ClientId, "1");
            Assert.AreEqual(result[0].Quantity, 5);

            result = ReportDataGenerator.RequestsInRange(requests, 5, 15);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 2);
        }
    }
}
