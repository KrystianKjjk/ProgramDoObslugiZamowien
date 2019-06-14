using Prism.Events;
using ProgramDoObslugiZamowien.Model;
using System.Collections.Generic;

namespace ProgramDoObslugiZamowien.UI.Event
{
    public class OpenFileReportViewEvent: PubSubEvent<OpenFileReportViewEventArgs>
    {
    }
    public class OpenFileReportViewEventArgs
    {
        //public List<Request> Requests { get; set; }
        public string FullFilePath { get; set; }
    }
}
