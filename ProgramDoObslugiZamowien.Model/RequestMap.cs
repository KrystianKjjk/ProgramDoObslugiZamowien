using ProgramDoObslugiZamowien.Model;
using System;

namespace ProgramDoObslugiZamowien.UI.Model
{
    public class RequestMap
    {
        public string ClientId { get; set; }
        public string RequestId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        
        ///returns "Request" if validate, otherwise return null
        public Request ValidateFields()
        {
            var request = new Request();
                bool Validate = false;
                Validate = request.SetValidateClientId(ClientId);
                if (!Validate) return null;
                Validate = request.SetValidateParseRequestId(RequestId);
                if (!Validate) return null;
                Validate = request.SetValidateName(Name);
                if (!Validate) return null;
                Validate = request.SetValidateParseQuantity(Quantity);
                if (!Validate) return null;
                Validate = request.SetValidateParsePrice(Price);
                if (!Validate) return null;

            return request;
        }
    }
}
