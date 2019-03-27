using System.Collections.Generic;
using System.IO;
using ProgramDoObslugiZamowien.Model;
using Newtonsoft.Json;
using ProgramDoObslugiZamowien.UI.Model;
using System.Windows;

namespace ProgramDoObslugiZamowien.UI.FileManagment
{
    public class JsonFileReader : FileReader
    {
        public JsonFileReader(string fullFilePath) : base(fullFilePath)
        {
            CheckFileExtention(".json");
        }
        public override List<Request> ReadAndValidateRequests()
        {
            var outputRequests = new List<Request>();
                using (StreamReader file = File.OpenText(_fullFilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var requests = (RequestCollection)serializer.Deserialize(file, typeof(RequestCollection));

                    foreach (var request in requests.Requests)
                    {
                        var validatedRequest = request.ValidateFields();
                        if (validatedRequest != null)
                            outputRequests.Add(validatedRequest);
                    }
                }
            return outputRequests;
        }
    }
    public class RequestCollection
    {
        private List<RequestMap> requests;
        public List<RequestMap> Requests { get => requests; set => requests = value; }
        public RequestCollection()
        {
            Requests = new List<RequestMap>();
        }
        public RequestCollection(List<Request> requests)
        {
            Requests = new List<RequestMap>();
            foreach (var request in requests)
            {
                Requests.Add(new RequestMap
                {
                    ClientId = request.ClientId,
                    RequestId=request.RequestId.ToString(),
                    Name = request.Name,
                    Quantity = request.Quantity.ToString(),
                    Price = request.Price.ToString()
                   
                });
            }
        }
    }    
}
