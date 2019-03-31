using ProgramDoObslugiZamowien.Model;
using System.Collections.Generic;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{
    public interface IReportViewModel
    {
        List<Request> Requests { get; set; }
        void Load(List<Request> requests, string fullFilePath);
    }
    
}
