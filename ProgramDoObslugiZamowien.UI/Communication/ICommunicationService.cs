namespace ProgramDoObslugiZamowien.UI.Communication
{
    public interface ICommunicationService
    {
        string AverageRequestsValue();
        string AverageRequestsValue(string clientId);
        string ListOfRequests();
        string ListOfRequests(string clientId);
        string RequestsInRange(string minPrice, string maxPrice);
        string RequestsInRange(string clientId, string minPrice, string maxPrice);
        string RequestsGroupedByName();
        string RequestsGroupedByName(string clientId);
        string SumOfRequests();
        string SumOfRequests(string clientId);
        string UnigueRequests();
        string UnigueRequests(string clientId);
        bool IsListOfData { get; }
    }
}