namespace TrancborderTimeCalc.ViewModel;

class MainWindowViewModel : Base.ViewModel
{
    #region Data

    private readonly IRepositoryService _repositoryService;
    private readonly IGeneratorService _generatorService;
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

    /// <summary> Эффективность массивов </summary>
    public ObservableCollection<Effective> Effectives { get; set; } = new();

    #region Места с массивами

    public string Massif2 => (Positions.SingleOrDefault(p => p.Number == 2)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 2)?.Massif.ToString()}" : "-";
    public string Massif3 => (Positions.SingleOrDefault(p => p.Number == 3)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 3)?.Massif.ToString()}" : "-";
    public string Massif4 => (Positions.SingleOrDefault(p => p.Number == 4)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 4)?.Massif.ToString()}" : "-";
    public string Massif5 => (Positions.SingleOrDefault(p => p.Number == 5)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 5)?.Massif.ToString()}" : "-";
    public string Massif6 => (Positions.SingleOrDefault(p => p.Number == 6)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 6)?.Massif.ToString()}" : "-";
    public string Massif8 => (Positions.SingleOrDefault(p => p.Number == 8)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 8)?.Massif.ToString()}" : "-";
    public string Massif10 => (Positions.SingleOrDefault(p => p.Number == 10)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 10)?.Massif.ToString()}" : "-";
    public string Massif12 => (Positions.SingleOrDefault(p => p.Number == 12)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 12)?.Massif.ToString()}" : "-";
    public string Massif14 => (Positions.SingleOrDefault(p => p.Number == 14)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 14)?.Massif.ToString()}" : "-";
    public string Massif15 => (Positions.SingleOrDefault(p => p.Number == 15)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 15)?.Massif.ToString()}" : "-";
    public string Massif16 => (Positions.SingleOrDefault(p => p.Number == 16)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 16)?.Massif.ToString()}" : "-";
    public string Massif18 => (Positions.SingleOrDefault(p => p.Number == 18)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 18)?.Massif.ToString()}" : "-";
    public string Massif19 => (Positions.SingleOrDefault(p => p.Number == 19)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 19)?.Massif.ToString()}" : "-";
    public string Massif20 => (Positions.SingleOrDefault(p => p.Number == 20)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 20)?.Massif.ToString()}" : "-";
    public string Massif21 => (Positions.SingleOrDefault(p => p.Number == 21)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 21)?.Massif.ToString()}" : "-";
    public string Massif22 => (Positions.SingleOrDefault(p => p.Number == 22)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 22)?.Massif.ToString()}" : "-";
    public string Massif23 => (Positions.SingleOrDefault(p => p.Number == 23)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 23)?.Massif.ToString()}" : "-";
    public string Massif24 => (Positions.SingleOrDefault(p => p.Number == 24)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 24)?.Massif.ToString()}" : "-";
    public string Massif25 => (Positions.SingleOrDefault(p => p.Number == 25)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 25)?.Massif.ToString()}" : "-";
    public string Massif26 => (Positions.SingleOrDefault(p => p.Number == 26)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 26)?.Massif.ToString()}" : "-";
    public string Massif27 => (Positions.SingleOrDefault(p => p.Number == 27)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 27)?.Massif.ToString()}" : "-";
    public string Massif28 => (Positions.SingleOrDefault(p => p.Number == 28)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 28)?.Massif.ToString()}" : "-";
    public string Massif29 => (Positions.SingleOrDefault(p => p.Number == 29)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 29)?.Massif.ToString()}" : "-";
    public string Massif30 => (Positions.SingleOrDefault(p => p.Number == 30)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 30)?.Massif.ToString()}" : "-";
    public string Massif31 => (Positions.SingleOrDefault(p => p.Number == 31)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 31)?.Massif.ToString()}" : "-";
    public string Massif32 => (Positions.SingleOrDefault(p => p.Number == 32)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 32)?.Massif.ToString()}" : "-";
    public string Massif33 => (Positions.SingleOrDefault(p => p.Number == 33)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 33)?.Massif.ToString()}" : "-";
    public string Massif34 => (Positions.SingleOrDefault(p => p.Number == 34)?.Massif > 0) ? $"массив № {Positions.SingleOrDefault(p => p.Number == 34)?.Massif.ToString()}" : "-";

    #endregion

    public int TimeToPlace
    {
        get => _settings.TimeToPlace;
        set
        {
            if (Equals(_settings.TimeToPlace, value)) return;
            _settings.TimeToPlace = value;
            OnPropertyChanged(nameof(TimeToPlace));
        }
    }
    public int TimeFromPlace
    {
        get => _settings.TimeFromPlace;
        set
        {
            if (Equals(_settings.TimeFromPlace, value)) return;
            _settings.TimeFromPlace = value;
            OnPropertyChanged(nameof(TimeFromPlace));
        }
    }
    public int TimeOnRow
    {
        get => _settings.TimeOnRow;
        set
        {
            if (Equals(_settings.TimeOnRow, value)) return;
            _settings.TimeOnRow = value;
            OnPropertyChanged(nameof(TimeOnRow));
        }
    }

    private int ResultMinimum = int.MaxValue;
    public string ResultComment
    {
        get
        {
            var seconds = Dos.Sum(d => d.Seconds);
            if (seconds < ResultMinimum - 3)
            {
                ResultMinimum = seconds;
                return "Превосходно!";
            }
            if (seconds < ResultMinimum)
            {
                ResultMinimum = seconds;
                return "Получше";
            }
            if (seconds == ResultMinimum)
            {
                return "Также";
            }
            if (seconds > ResultMinimum + 3)
            {
                return "Гораздо хуже!";
            }
            if (seconds > ResultMinimum + 10)
            {
                return "ХУЖЕ НЕКУДА!";
            }
            return "Хуже.";
        }
    }

    public int ResultMinutes => Dos.Sum(d => d.Seconds) / 60;
    public int ResultSeconds => Dos.Sum(d => d.Seconds) % 60;


    public double SpeedOnMassif
    {
        get
        {
            double count = Dos.Count(d => d.MassifNumber != 0);
            double time = Dos.Sum(d => d.Seconds) / 60.0;
            return count / time;
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

    public MainWindowViewModel(IRepositoryService repositoryService, ISettings settings, IGeneratorService generatorService)
    {
        _repositoryService = repositoryService;
        _settings = settings;
        _generatorService = generatorService;
        LoadData();

    }

    #region Commands

    private ICommand _LoadBasicDataCommand;
    /// <summary> Загрузить начальные эталонные данные </summary>
    public ICommand LoadBasicDataCommand => _LoadBasicDataCommand ??=
        new LambdaCommand(OnLoadBasicDataCommandExecuted, CanLoadBasicDataCommandExecute);
    private bool CanLoadBasicDataCommandExecute(object p) => true;
    private void OnLoadBasicDataCommandExecuted(object p)
    {
        LoadData(true);
        OnUpdatePlacesCommandExecuted(p);
    }

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
        OnPropertyChanged(nameof(Massif15));
        OnPropertyChanged(nameof(Massif16));
        OnPropertyChanged(nameof(Massif18));
        OnPropertyChanged(nameof(Massif19));
        OnPropertyChanged(nameof(Massif20));
        OnPropertyChanged(nameof(Massif21));
        OnPropertyChanged(nameof(Massif22));
        OnPropertyChanged(nameof(Massif23));
        OnPropertyChanged(nameof(Massif24));
        OnPropertyChanged(nameof(Massif25));
        OnPropertyChanged(nameof(Massif26));
        OnPropertyChanged(nameof(Massif27));
        OnPropertyChanged(nameof(Massif28));
        OnPropertyChanged(nameof(Massif29));
        OnPropertyChanged(nameof(Massif30));
        OnPropertyChanged(nameof(Massif31));
        OnPropertyChanged(nameof(Massif32));
        OnPropertyChanged(nameof(Massif33));
        OnPropertyChanged(nameof(Massif34));
    }

    private ICommand _GenerateStepsCommand;
    /// <summary> Сгенерировать шаги и рассчитать результаты </summary>
    public ICommand GenerateStepsCommand => _GenerateStepsCommand ??=
        new LambdaCommand(OnGenerateStepsCommandExecuted, CanGenerateStepsCommandExecute);
    private bool CanGenerateStepsCommandExecute(object p) => true;
    private void OnGenerateStepsCommandExecuted(object p)
    {
        var minMassif = Positions.Where(p => p.Massif != 0).Min(p => p.Massif);
        var maxMassif = Positions.Max(p => p.Massif);
        for (int i = minMassif; i <= maxMassif; i++)
        {
            var count = Positions.Count(p => p.Massif == i);
            if (count != 1)
            {
                if (count > 1)
                    App.Services.GetService<IDialogService>()?.ShowInfo($"Неправильные входные данные. Больше одного массива с одинаковым номером № {i} во входных данных.");
                if (count < 1)
                    App.Services.GetService<IDialogService>()?.ShowInfo($"Неправильные входные данные. Нехватает массива с № {i} во входных данных массивов.");
                return;
            }
        }
        Dos.Clear();
        var dos = _generatorService.GenerateDos(Positions);
        foreach (var item in dos)
            Dos.Add(item);
        OnPropertyChanged(nameof(ResultMinutes));
        OnPropertyChanged(nameof(ResultSeconds));
        OnPropertyChanged(nameof(SpeedOnMassif));
        OnPropertyChanged(nameof(ResultComment));
        Effectives.Clear();
        foreach (var item in _generatorService.CalcEffectives(dos))
            Effectives.Add(item);

        App.Services.GetService<IDialogService>()?.ShowInfo($"Шаги сгенерированы, рассчеты выполнены.\nВремя: {ResultMinutes} мин. {ResultSeconds} сек., скорость: {SpeedOnMassif:F4} массивов/минуту.\n{ResultComment}");
    }

    private ICommand _ShowAboutCommand;
    /// <summary> Открыть приложение о программе </summary>
    public ICommand ShowAboutCommand => _ShowAboutCommand ??=
        new LambdaCommand(OnShowAboutCommandExecuted, CanShowAboutCommandExecute);
    private bool CanShowAboutCommandExecute(object p) => true;
    private void OnShowAboutCommandExecuted(object p)
    {
        var form = new About();
        form.ShowDialog();
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

    private void LoadData(bool initLoad = false)
    {
        Positions.Clear();
        var postions = (initLoad) 
            ? _repositoryService.GetInitPositions() 
            : _repositoryService.GetPositions();
        foreach (var item in postions)
            Positions.Add(item);
        Dos.Clear();
        var dos = _generatorService.GenerateDos(Positions);
        foreach (var item in dos)
            Dos.Add(item);
        Effectives.Clear();
        foreach (var item in _generatorService.CalcEffectives(dos))
            Effectives.Add(item);
    }

    #endregion
}

