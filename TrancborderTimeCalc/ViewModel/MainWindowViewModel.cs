using System.Collections.ObjectModel;
using System.Windows.Input;
using TrancborderTimeCalc.Command;

namespace TrancborderTimeCalc.ViewModel;

class MainWindowViewModel : Base.ViewModel
{
    #region Data

    private readonly RepositoryService _repositoryService;
    private readonly ISettings _settings;

    #endregion

    #region Properties

    /// <summary> Позиции установки трансбордера </summary>
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

    /// <summary> Операции трансбордера </summary>
    public ObservableCollection<Do> Dos { get; set; } = new ();
    private Do _SelectedDo;
    /// <summary> Выбранная в списке операция </summary>
    public Do SelectedDo
    {
        get => _SelectedDo;
        set => Set(ref _SelectedDo, value);
    }


    private string _Title = "Модель камеры созревания";
    /// <summary> Заголовок </summary>
    public string Title
    {
        get => _Title;
        set => Set(ref _Title, value);
    }

    #endregion

    public MainWindowViewModel(RepositoryService repositoryService, ISettings settings)
    {
        _repositoryService = repositoryService;
        _settings = settings;
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
        Dos.Clear();
        var i = 1;
        foreach (var item in _repositoryService.GetDos())
        {
            item.Order = i++;
            Dos.Add(item);
        }
        var row = 9;
        foreach (var item in Dos)
        {
            var sec = 0;
            var sb = new StringBuilder();
            //to 1
            if (item.Doing == TypeDoing.FromInputToHitTable1 || item.Doing == TypeDoing.FromInputToHitTable2 || item.Doing == TypeDoing.FromInputToPosition)
            {
                sec += row * _settings.TimeOnRow;
                row = 1;                
            }
            if (item.Doing == TypeDoing.FromTable1ToPosition)
            {
                sec += Math.Abs(row - 6) * _settings.TimeOnRow;
                row = 6;
            }
            if (item.Doing == TypeDoing.FromTable2ToPosition)
            {
                sec += Math.Abs(row - 5) * _settings.TimeOnRow;
                row = 5;
            }
            if (item.Doing == TypeDoing.FromPositionToOutput)
            {
                var nextrow = Positions.Single(p => p.Number == item.PositionNumber).Row;
                sec += Math.Abs(row - nextrow) * _settings.TimeOnRow;
                row = nextrow;                
            }
            sb.Append(sec.ToString());
            //catch
            sec += _settings.TimeFromPlace;
            sb.Append($" + {_settings.TimeFromPlace}");
            var sec2 = 0;
            //to 2
            if (item.Doing == TypeDoing.FromInputToHitTable1)
            {
                sec2 += Math.Abs(row - 6) * _settings.TimeOnRow;
                row = 6;
            }
            if (item.Doing == TypeDoing.FromInputToHitTable2)
            {
                sec2 += Math.Abs(row - 5) * _settings.TimeOnRow;
                row = 5;
            }
            if (item.Doing == TypeDoing.FromInputToPosition || item.Doing == TypeDoing.FromTable1ToPosition || item.Doing == TypeDoing.FromTable2ToPosition)
            {
                var nextrow = Positions.Single(p => p.Number == item.PositionNumber).Row;
                sec2 += Math.Abs(row - nextrow) * _settings.TimeOnRow;
                row = nextrow;
            }
            if (item.Doing == TypeDoing.FromPositionToOutput)
            {
                sec2 += Math.Abs(row - 9) * _settings.TimeOnRow;
                row = 9;
            }
            sb.Append($" + {sec2}");
            //throw
            sec2 += _settings.TimeToPlace;
            sb.Append($" + {_settings.TimeToPlace}");

            item.Seconds = sec + sec2;
            item.Calc = sb.ToString();
        }
    }

    #endregion
}

