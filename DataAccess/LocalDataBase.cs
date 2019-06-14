using ProgramDoObslugiZamowien.Model;
using System.Collections.Generic;

namespace ProgramDoObslugiZamowien.DataAccess
{
    public class LocalDataBase
    {
        public LocalDataBase()
        {
            Database = new Dictionary<string, List<Request>>();
        }
        public Dictionary<string, List<Request>> Database { get; set; }
    }
}
