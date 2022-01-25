namespace TrancborderTimeCalc.Services;

public class WindowDialogService : IDialogService
{
    public void ShowInfo(string message)
    {
        MessageBox.Show(message);
    }
}

