using ProgramDoObslugiZamowien.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
