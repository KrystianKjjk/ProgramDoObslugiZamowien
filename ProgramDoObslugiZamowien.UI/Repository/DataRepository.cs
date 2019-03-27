using ProgramDoObslugiZamowien.DataAccess;
using ProgramDoObslugiZamowien.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramDoObslugiZamowien.UI.Repository
{
    public class DataRepository : IDataRepository
    {
        LocalDataBase Context;
        public DataRepository()
        {
            Context = new LocalDataBase();
        }
        public void AddData(string fullPathName, List<Request> requests)
        {
            Context.Database.Add(fullPathName, requests);
        }
    }
}
