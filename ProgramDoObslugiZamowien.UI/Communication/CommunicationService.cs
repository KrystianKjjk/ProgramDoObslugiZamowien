using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramDoObslugiZamowien.UI.Communication
{
    public class CommunicationService : ICommunicationService
    {
        public string RequestsGroupedByName()
        {
            ListOfData = false;
            return $"Ilość zamówień pogrupowanych po nazwie";
        }

        public string RequestsGroupedByName(string clientId)
        {
            ListOfData = false;
            return $"Ilość zamówień pogrupowanych po dla klienta: {clientId}";
        }

        public string RequestsInRange(string minPrice, string maxPrice)
        {
            ListOfData = true;
            return $"Zamówienia w przedziale {minPrice}-{maxPrice}";
        }

        public string RequestsInRange(string clientId, string minPrice, string maxPrice)
        {
            ListOfData = true;
            return $"Zamówienia w przedziale {minPrice}-{maxPrice} dla klienta: {clientId}";
        }

        public string AverageRequestsValue()
        {
            ListOfData = false;
            return $"Średnia wartość zamówień";
        }

        public string AverageRequestsValue(string clientId)
        {
            ListOfData = false;
            return $"Średnia wartość zamówień dla klienta: {clientId}";
        }

        public string ListOfRequests()
        {
            ListOfData = true;
            return "Lista zamówień";
        }

        public string ListOfRequests(string clientId)
        {
            ListOfData = true;
            return $"Lista zamówień dla klienta: {clientId}";
        }

        public string SumOfRequests()
        {
            ListOfData = false;
            return "Łączna kwota zamówień";
        }

        public string SumOfRequests(string clientId)
        {
            ListOfData = false;
            return $"Łączna kwota zamówień dla klienta: {clientId}";
        }

        public string UnigueRequests()
        {
            ListOfData = false;
            return "Ilość wszystkich zamówień";
        }

        public string UnigueRequests(string clientId)
        {
            ListOfData = false;
            return $"Ilość wszystkich zamówień dla klienta: {clientId}";
        }

        public bool ListOfData { get; private set; }

    }
}
