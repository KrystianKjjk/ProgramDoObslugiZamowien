using ProgramDoObslugiZamowien.DataAccess;
using ProgramDoObslugiZamowien.Model;
using System.Collections.Generic;

namespace ProgramDoObslugiZamowien.UI.Repository
{
    public class DataRepository : IDataRepository
    {
        LocalDataBase Context;
        public DataRepository()
        {
            Context = new LocalDataBase();
        }
        public List<Request> GetData (string fullFilePath)
        {
            return Context.Database[fullFilePath];
        }
        public void AddData(string fullPathName, List<Request> requests)
        {
            Context.Database.Add(fullPathName, requests);
        }

        public void RemoveData(string fullFilePath)
        {
            Context.Database.Remove(fullFilePath);
        }
    }
}
