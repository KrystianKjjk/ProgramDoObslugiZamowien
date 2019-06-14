using Prism.Events;

namespace ProgramDoObslugiZamowien.UI.Event
{
    public class RemoveFileEvent : PubSubEvent<RemoveFileEventArgs>
    {
    }

    public class RemoveFileEventArgs
    {
        public string FullFilePath { get; set; }
        public int Value { get; set; }
    }
}
