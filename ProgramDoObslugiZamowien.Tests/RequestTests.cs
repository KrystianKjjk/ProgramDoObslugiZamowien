using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramDoObslugiZamowien.Model;

namespace ProgramDoObslugiZamowien.Tests
{
    [TestClass]
    public class RequestTests
    {
        [TestMethod]
        public void SetValidateClientIdTest()
        {
            var request = new Request();
            var clientId = "1qwrAS";
            var result = request.SetValidateClientId(clientId);

            Assert.IsTrue(result);
            Assert.AreEqual(clientId, request.ClientId);

            clientId = "123 d";
            result = request.SetValidateClientId(clientId);
            Assert.IsFalse(result);
            Assert.AreNotEqual(clientId, request.ClientId);

            clientId = "d;dd";
            result = request.SetValidateClientId(clientId);
            Assert.IsFalse(result);
            Assert.AreNotEqual(clientId, request.ClientId);
        }
        [TestMethod]
        public void SetValidateRequestsTest()
        {
            var request = new Request();

            var requestId = "123";
            var result = request.SetValidateParseRequestId(requestId);
            Assert.IsTrue(result);
            Assert.AreEqual(long.Parse(requestId), request.RequestId);

            var NotValidrequestId = "123bb";
            result = request.SetValidateParseRequestId(NotValidrequestId);
            Assert.IsFalse(result);
            Assert.AreEqual(long.Parse(requestId), request.RequestId);

        }
        [TestMethod]
        public void SetValidateNameTest()
        {
            var request = new Request();

            var Name = "123ss kkf";
            var result = request.SetValidateName(Name);
            Assert.IsTrue(result);
            Assert.AreEqual(Name, request.Name);

            var NotValidrequestId = "12w ] 3bb";
            result = request.SetValidateName(NotValidrequestId);
            Assert.IsFalse(result);
            Assert.AreEqual(Name, request.Name);
        }
        [TestMethod]
        public void SetValidateParseQuantityTest()
        {
            var request = new Request();
            string quantity = "22";
            var result = request.SetValidateParseQuantity(quantity);

            Assert.IsTrue(result);
            Assert.AreEqual(quantity, request.Quantity.ToString());

            quantity = "33d";
            result = request.SetValidateParseQuantity(quantity);

            Assert.IsFalse(result);
            Assert.AreNotEqual(quantity, request.Quantity.ToString());
        }
        [TestMethod]
        public void SetValidateParsePriceTest()
        {
            var request = new Request();
            string price = "22";
            var result = request.SetValidateParsePrice(price);

            Assert.IsTrue(result);
            Assert.AreEqual(price, request.Price.ToString());


            price = "33d";
            result = request.SetValidateParsePrice(price);

            Assert.IsFalse(result);
            Assert.AreNotEqual(price, request.Price.ToString());

        }
        public void ValidateRequestMapFieldsTest()
        {
            var validatedRequestMap = new RequestMap
            {
                ClientId = "1",
                RequestId ="1",
                Name = "Bulka i chleb",
                Quantity="5",
                Price="10.3"
            };
            var request = validatedRequestMap.ValidateFields();

            Assert.IsNotNull(request);

            var NotValidatedRequestMap = new RequestMap
            {
                ClientId = "1 vv",
                RequestId = "1",
                Name = "Bulka i chleb",
                Quantity = "5",
                Price = "10.3"
            };
            request = validatedRequestMap.ValidateFields();

            Assert.IsNull(request);
        }
    }
}
