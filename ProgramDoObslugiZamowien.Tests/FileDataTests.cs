using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramDoObslugiZamowien.UI.FileManagment;

namespace ProgramDoObslugiZamowien.Tests
{
    [TestClass]
    public class FileDataTests
    {
        private string file = @"..\..\..\PlikiZDanymi\\plikJSON.json";
        [TestMethod]
        public void GetFileNameTest()
        {
            var result = FileData.GetFileName(file);
            var fileName = "plikJSON.json";

            Assert.AreEqual(fileName, result);
        }
        [TestMethod]
        public void GetFileExtentionTest()
        {
            var result = FileData.GetFileExtention(file);
            var fileExt = ".json";

            Assert.AreEqual(fileExt, result);
        }
        [TestMethod]
        public void IsAvailableFileExtentionTest()
        {
            var result = FileData.IsAvailableFileExtention(file);

            Assert.IsTrue(result);
            var txtFile = "WrongExt.txt";
            result = FileData.IsAvailableFileExtention(txtFile);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void GetRequestsFromFileTest()
        {
            var result = FileData.GetRequestsFromFile(file);
            var singleRequest = result[0];

            Assert.AreEqual("1", singleRequest.ClientId);
            Assert.AreEqual(1, singleRequest.RequestId);
            Assert.AreEqual("Bulka kulka", singleRequest.Name);
            Assert.AreEqual(1, singleRequest.Quantity);
            Assert.AreEqual(10.5, singleRequest.Price);

        }
    }    
}
