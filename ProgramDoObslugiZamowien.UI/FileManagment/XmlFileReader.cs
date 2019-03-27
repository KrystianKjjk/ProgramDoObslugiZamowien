using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using ProgramDoObslugiZamowien.Model;

namespace ProgramDoObslugiZamowien.UI.FileManagment
{
    class XmlFileReader : FileReader
    {
        public XmlFileReader(string fullFilePath) : base(fullFilePath)
        {
            CheckFileExtention(".xml");
        }
        public override List<Request> ReadAndValidateRequests()
        {
            var requests = new requests();
            XmlSerializer serializer = new XmlSerializer(typeof(Request));

            using (FileStream fs = File.OpenRead(_fullFilePath))
            {
                requests = (requests)new XmlSerializer(typeof(requests)).Deserialize(fs);
            }
            return requests.ValidateDatAndMapToRequestList(requests);
        }
    }
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class requests
    {

        private List<requestsRequest> requestField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("request")]
        public List<requestsRequest> request
        {
            get
            {
                return this.requestField;
            }
            set
            {
                this.requestField = value;
            }
        }
        public List<Request> ValidateDatAndMapToRequestList(requests requests)
        {
            var outputRequest = new List<Request>();
            foreach (var requestsRequest in requests.request)
            {
                var request = new Request();
                bool Validate = false;
                Validate = request.SetValidateClientId(requestsRequest.clientId);
                if (!Validate) continue;
                Validate = request.SetValidateParseRequestId(requestsRequest.requestId);
                if (!Validate) continue;
                Validate = request.SetValidateName(requestsRequest.name);
                if (!Validate) continue;
                Validate = request.SetValidateParseQuantity(requestsRequest.quantity);
                if (!Validate) continue;
                Validate = request.SetValidateParsePrice(requestsRequest.price);
                if (!Validate) continue;

                    outputRequest.Add(request);

            }

            return outputRequest;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class requestsRequest
    {

        private string clientIdField;
        private string requestIdField;
        private string nameField;
        private string quantityField;
        private string priceField;
        /// <remarks/>
        public string clientId
        {
            get
            {
                return this.clientIdField;
            }
            set
            {
                this.clientIdField = value;
            }
        }
        /// <remarks/>
        public string requestId
        {
            get
            {
                return this.requestIdField;
            }
            set
            {
                this.requestIdField = value;
            }
        }
        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        /// <remarks/>
        public string quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }
        /// <remarks/>
        public string price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }
    }
}
