using ProgramDoObslugiZamowien.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{
    public interface IReportViewModel
    {
        List<Request> Requests { get; set; }
        void Load(List<Request> requests);
    }
    
}
