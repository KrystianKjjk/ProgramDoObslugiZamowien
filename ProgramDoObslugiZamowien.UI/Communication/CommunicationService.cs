namespace ProgramDoObslugiZamowien.UI.Communication
{
    public class CommunicationService : ICommunicationService
    {
        public string RequestsGroupedByName()
        {
            IsListOfData = false;
            return $"Ilość zamówień pogrupowanych po nazwie";
        }

        public string RequestsGroupedByName(string clientId)
        {
            IsListOfData = false;
            return $"Ilość zamówień pogrupowanych po nazwie dla klienta: {clientId}";
        }

        public string RequestsInRange(string minPrice, string maxPrice)
        {
            IsListOfData = true;
            return $"Zamówienia w przedziale {minPrice}-{maxPrice}";
        }

        public string RequestsInRange(string clientId, string minPrice, string maxPrice)
        {
            IsListOfData = true;
            return $"Zamówienia w przedziale {minPrice}-{maxPrice} dla klienta: {clientId}";
        }

        public string AverageRequestsValue()
        {
            IsListOfData = false;
            return $"Średnia wartość zamówień";
        }

        public string AverageRequestsValue(string clientId)
        {
            IsListOfData = false;
            return $"Średnia wartość zamówień dla klienta: {clientId}";
        }

        public string ListOfRequests()
        {
            IsListOfData = true;
            return "Lista zamówień";
        }

        public string ListOfRequests(string clientId)
        {
            IsListOfData = true;
            return $"Lista zamówień dla klienta: {clientId}";
        }

        public string SumOfRequests()
        {
            IsListOfData = false;
            return "Łączna kwota zamówień";
        }

        public string SumOfRequests(string clientId)
        {
            IsListOfData = false;
            return $"Łączna kwota zamówień dla klienta: {clientId}";
        }

        public string UnigueRequests()
        {
            IsListOfData = false;
            return "Ilość wszystkich zamówień";
        }

        public string UnigueRequests(string clientId)
        {
            IsListOfData = false;
            return $"Ilość wszystkich zamówień dla klienta: {clientId}";
        }

        public bool IsListOfData { get; private set; }

    }
}
