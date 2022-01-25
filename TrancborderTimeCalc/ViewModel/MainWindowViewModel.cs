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

    #region Места с массивами

    public string Massif2 => (Positions.SingleOrDefault(p => p.Number == 2)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 2)?.Massif.ToString()}" : "отсутствует";
    public string Massif3 => (Positions.SingleOrDefault(p => p.Number == 3)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 3)?.Massif.ToString()}" : "отсутствует";
    public string Massif4 => (Positions.SingleOrDefault(p => p.Number == 4)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 4)?.Massif.ToString()}" : "отсутствует";
    public string Massif5 => (Positions.SingleOrDefault(p => p.Number == 5)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 5)?.Massif.ToString()}" : "отсутствует";
    public string Massif6 => (Positions.SingleOrDefault(p => p.Number == 6)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 6)?.Massif.ToString()}" : "отсутствует";
    public string Massif8 => (Positions.SingleOrDefault(p => p.Number == 8)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 8)?.Massif.ToString()}" : "отсутствует";
    public string Massif10 => (Positions.SingleOrDefault(p => p.Number == 10)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 10)?.Massif.ToString()}" : "отсутствует";
    public string Massif12 => (Positions.SingleOrDefault(p => p.Number == 12)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 12)?.Massif.ToString()}" : "отсутствует";
    public string Massif14 => (Positions.SingleOrDefault(p => p.Number == 14)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 14)?.Massif.ToString()}" : "отсутствует";




    #endregion


    public int TimeToPlace => _settings.TimeToPlace;
    public int TimeFromPlace => _settings.TimeFromPlace;
    public int TimeOnRow => _settings.TimeOnRow;


    public int ResultMinutes => Dos.Sum(d => d.Seconds) / 60;
    public int ResultSeconds => Dos.Sum(d => d.Seconds) % 60;


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

    private ICommand _UpdatePlacesCommand;
    /// <summary> Обновить графическое изображение мест хранения массивов </summary>
    public ICommand UpdatePlacesCommand => _UpdatePlacesCommand ??=
        new LambdaCommand(OnUpdatePlacesCommandExecuted, CanUpdatePlacesCommandExecute);
    private bool CanUpdatePlacesCommandExecute(object p) => true;
    private void OnUpdatePlacesCommandExecuted(object p)
    {
        OnPropertyChanged(nameof(Massif2));
        OnPropertyChanged(nameof(Massif3));
        OnPropertyChanged(nameof(Massif4));
        OnPropertyChanged(nameof(Massif5));
        OnPropertyChanged(nameof(Massif6));
        OnPropertyChanged(nameof(Massif8));
        OnPropertyChanged(nameof(Massif10));
        OnPropertyChanged(nameof(Massif12));
        OnPropertyChanged(nameof(Massif14));


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
                var nextrow = Positions.Single(p => p.Massif == item.MassifNumber).Row;
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
                var nextrow = Positions.Single(p => p.Massif == item.MassifNumber).Row;
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

