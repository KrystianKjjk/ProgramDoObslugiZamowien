using CsvHelper;
using ProgramDoObslugiZamowien.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProgramDoObslugiZamowien.UI.FileManagment
{
    public class CsvFileReader : FileReader
    {        
        public CsvFileReader(string fullFilePath) : base(fullFilePath)
        {
            CheckFileExtention(".csv");
        }

        public override List<Request> ReadAndValidateRequests()
        {
            var outputRequests = new List<Request>();
            using (var streamReader = File.OpenText(_fullFilePath))
            {
                var reader = new CsvReader(streamReader);

                var requests = reader.GetRecords<RequestMap>().ToList();
                foreach (var request in requests)
                {
                    var validatedRequest = request.ValidateFields();
                    if (validatedRequest != null)
                        outputRequests.Add(validatedRequest);
                }
            }
            return outputRequests;
        }
    }
}
