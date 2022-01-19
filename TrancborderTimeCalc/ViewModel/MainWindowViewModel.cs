using System.Collections.ObjectModel;
using System.Windows.Input;
using TrancborderTimeCalc.Command;

namespace TrancborderTimeCalc.ViewModel;

class MainWindowViewModel : Base.ViewModel
{
    #region Data

    private readonly RepositoryService _repositoryService;

    #endregion

    #region Properties

    public ObservableCollection<Position> Positions { get; set; } = new ();
    private Position _selectPosition;
    /// <summary> Выбранная в списке текущая позиция трансбордера </summary>
    public Position SelectPosition
    {
        get => _selectPosition;
        set
        {
            Set(ref _selectPosition, value);
        }
    }

    private string _Title = "Модель камеры созревания";
    /// <summary> Заголовок </summary>
    public string Title
    {
        get => _Title;
        set => Set(ref _Title, value);
    }

    #endregion

    public MainWindowViewModel(RepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
        LoadData();

    }

    #region Commands

    private ICommand _UpMassifCommand;
    /// <summary> Увеличить значение массива на еденицу </summary>
    public ICommand UpMassifCommand => _UpMassifCommand ??=
        new LambdaCommand(OnUpMassifCommandExecuted, CanUpMassifCommandExecute);
    private bool CanUpMassifCommandExecute(object p) => SelectPosition is { };
    private void OnUpMassifCommandExecuted(object p)
    {
        SelectPosition.Massif += 1;
        if (SelectPosition.Massif > 100)
            SelectPosition.Massif = 100;
    }

    private ICommand _DownMassifCommand;
    /// <summary> Уменьшить значение массива на еденицу </summary>
    public ICommand DownMassifCommand => _DownMassifCommand ??=
        new LambdaCommand(OnDownMassifCommandExecuted, CanDownMassifCommandExecute);
    private bool CanDownMassifCommandExecute(object p) => SelectPosition is { };
    private void OnDownMassifCommandExecuted(object p)
    {
        SelectPosition.Massif -= 1;
        if (SelectPosition.Massif < 0)
            SelectPosition.Massif = 0;
    }

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

    private void LoadData()
    {
        Positions.Clear();
        foreach (var item in _repositoryService.GetPositions())
            Positions.Add(item);
    }

    #endregion
}

