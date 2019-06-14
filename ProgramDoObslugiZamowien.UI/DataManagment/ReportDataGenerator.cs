using ProgramDoObslugiZamowien.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProgramDoObslugiZamowien.UI.DataManagment
{
    public static class ReportDataGenerator
    {
        public static List<Request> GetRequests(List<Request> requests, string clientId)
        {
            return requests.Where(f => f.ClientId == clientId).ToList();
        }
        public static double SumOfRequestsPrice(List<Request> requests)
        {
            return requests.Sum(r => r.Price * r.Quantity);
        }
        public static double SumOfRequestsPrice(List<Request> requests, string clientId)
        {
            var clientRequest = GetRequests(requests, clientId);
            return SumOfRequestsPrice(clientRequest);
        }
        public static int NoOfUniqeRequests(List<Request> requests, string clientId)
        {
            List<long> vs = new List<long>();

            foreach (var request in GetRequests(requests, clientId))
            {
                if (!vs.Contains(request.RequestId))
                    vs.Add(request.RequestId);
            }
            return vs.Count();
        }
        public static int NoOfUniqeRequests(List<Request> requests)
        {
            List<string> clients = new List<string>();

            foreach (var request in requests)// find all clients
            {
                if (!clients.Contains(request.ClientId))
                    clients.Add(request.ClientId);
            }
            return clients.Sum(c => NoOfUniqeRequests(requests, c));
        }
        public static int NoOfRequestGroupedByName(List<Request>requests, string clientId = null)
        {
            int FindNumber(List<Request> req)
            {
                var listOfNames = new List<string>();
                foreach (var request in req)
                {
                    if (!listOfNames.Contains(request.Name))
                        listOfNames.Add(request.Name);
                }
                return listOfNames.Count();
            }
            if (clientId != null)
                return FindNumber(GetRequests(requests, clientId));
            return FindNumber(requests);
        }
        public static double AverageRequests(List<Request> requests)
        {
            return Floor(SumOfRequestsPrice(requests) / NoOfUniqeRequests(requests));
        }
        public static double AverageRequests(List<Request> requests, string clientId)
        {
            var clientRequests = GetRequests(requests, clientId);
            return Floor(SumOfRequestsPrice(clientRequests) / NoOfUniqeRequests(requests, clientId));        }
        public static List<Request> RequestsInRange(List<Request> requests, double minPrice, double maxPrice)
        {
            return requests
                .Where(f => f.Price * f.Quantity >= minPrice && f.Price * f.Quantity <= maxPrice).ToList();
        }
        public static List<Request> RequestsInRange(List<Request> requests, double minPrice, double maxPrice, string clientId)
        {
            var requestForClient = GetRequests(requests, clientId);
            return RequestsInRange(requestForClient, minPrice, maxPrice);
        }
        public static List<Request> Sort(List<Request> requests, string property)
        {
            return requests.OrderBy(f => typeof(Request).GetProperty(property).GetValue(f)).ToList();
        }
        private static double Floor(double data)
        {
            return System.Math.Floor(data * 100) / 100;
        }
    }
}
