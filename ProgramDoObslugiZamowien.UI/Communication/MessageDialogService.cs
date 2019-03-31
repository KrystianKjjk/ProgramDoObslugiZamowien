using System.Windows;

namespace ProgramDoObslugiZamowien.UI.Communication
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK
                ? MessageDialogResult.OK
                : MessageDialogResult.Cancel;
        }
        public void ShowInfoDialog(string text)
        {
            MessageBox.Show(text, "Informacja");
        }
    }
    public enum MessageDialogResult
    {
        Cancel,
        OK
    }
}
