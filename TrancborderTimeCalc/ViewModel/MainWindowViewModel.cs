using System.Windows.Input;
using TrancborderTimeCalc.Command;

namespace TrancborderTimeCalc.ViewModel;

class MainWindowViewModel : Base.ViewModel
{
    #region Data

    #endregion

    #region Properties

    private string _Title = "Модель камеры созревания";
    /// <summary> Заголовок </summary>
    public string Title
    {
        get => _Title;
        set => Set(ref _Title, value);
    }

    #endregion

    public MainWindowViewModel()
    {

    }

    #region Commands

    private ICommand _CloseAppCommand;
    /// <summary> Закрыть приложение </summary>
    public ICommand CloseAppCommand => _CloseAppCommand ??=
        new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
    private bool CanCloseAppCommandExecute(object p) => true;
    private void OnCloseAppCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }
    

    #endregion

    #region Support

    #endregion
}

