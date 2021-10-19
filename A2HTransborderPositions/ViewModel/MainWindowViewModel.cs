using A2HTransborderPositions.Commands;
using A2HTransborderPositions.Commands.Base;
using A2HTransborderPositions.Interfaces;
using A2HTransborderPositions.Models;
using A2HTransborderPositions.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace A2HTransborderPositions.ViewModel
{
    class MainWindowViewModel : Base.ViewModel
    {
        #region Данные

        private readonly IRepositoryService _repositoryService;
        private readonly IMixReaderService _mixReaderService;
        private Timer _timer;

        #endregion Данные

        #region Свойства

        public ObservableCollection<Position> Positions { get; } = new ();

        private Position _selectPosition;
        /// <summary> Выбранная в списке текущая позиция трансбордера </summary>
        public Position SelectPosition
        {
            get => _selectPosition;
            set => Set( ref _selectPosition, value );
        }

        private int _currentPosition;
        /// <summary> Текущая позиция трансбордера </summary>
        public int CurrentPosition
        {
            get => _currentPosition;
            set => Set(ref _currentPosition, value);
        }

        private string _targetPlace;
        /// <summary> Цель место трансбордера </summary>
        public string TargetPlace
        {
            get => _targetPlace;
            set => Set(ref _targetPlace, value);
        }

        private string _fixerLeftFixed;
        /// <summary> Центратор слева закрыт - зафиксирован </summary>
        public string FixerLeftFixed
        {
            get => _fixerLeftFixed;
            set => Set(ref _fixerLeftFixed, value);
        }

        private string _fixerRightFixed;
        /// <summary> Центратор справа закрыт - зафиксирован </summary>
        public string FixerRightFixed
        {
            get => _fixerRightFixed;
            set => Set(ref _fixerRightFixed, value);
        }

        #region Поддержка

        private string _Title = "A2H Утилита настройки позиций трансбордера";
        /// <summary> Заголовок приложения </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion Поддержка

        #endregion Свойства

        public MainWindowViewModel(IRepositoryService repositoryService, IMixReaderService mixReaaderService)
        {
            _repositoryService = repositoryService;
            _mixReaderService = mixReaaderService;

            LoadData();

            _timer = new Timer(200);
            _timer.Elapsed += _timer_Elapsed;
        }

        #region Команды

        private ICommand _UpdateApplicationCommand;
        /// <summary> Обновить позиции трансбордера </summary>
        public ICommand UpdateApplicationCommand => _UpdateApplicationCommand ??=
            new LambdaCommand(OnUpdateApplicationCommandExecuted, CanUpdateApplicationCommandExecute);
        private bool asyncExecuteUpdateApplicationCommand = false;
        private bool CanUpdateApplicationCommandExecute(object p) => !asyncExecuteUpdateApplicationCommand;
        private async void OnUpdateApplicationCommandExecuted(object p)
        {
            asyncExecuteUpdateApplicationCommand = true;
            try
            {
                await Task.Run(() =>
                {
                    var values = new int[32];
                    _mixReaderService.GetCurrentPositions(out int error, values);
                    _repositoryService.SetCurrentPositions(values);
                    asyncExecuteUpdateApplicationCommand = false;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка в работе приложения: \n{ex.Message}");
                asyncExecuteUpdateApplicationCommand = false;
                throw;
            }
        }

        private ICommand _RunApplicationCommand;
        /// <summary> Начать получать актуальные данные трансбордера </summary>
        public ICommand RunApplicationCommand => _RunApplicationCommand ??=
            new LambdaCommand(OnRunApplicationCommandExecuted, CanRunApplicationCommandExecute);
        private bool CanRunApplicationCommandExecute(object p) => true;
        private void OnRunApplicationCommandExecuted(object p)
        { 
            _timer.Start();
        }

        private ICommand _StopApplicationCommand;
        /// <summary> Начать получать актуальные данные трансбордера </summary>
        public ICommand StopApplicationCommand => _StopApplicationCommand ??=
            new LambdaCommand(OnStopApplicationCommandExecuted, CanStopApplicationCommandExecute);
        private bool CanStopApplicationCommandExecute(object p) => true;
        private void OnStopApplicationCommandExecuted(object p)
        {
            _timer.Stop();
        }


        private ICommand _UpSetPositionCommand;
        /// <summary> Увеличить позицию задания на еденицу </summary>
        public ICommand UpSetPositionCommand => _UpSetPositionCommand ??=
            new LambdaCommand(OnUpSetPositionCommandExecuted, CanUpSetPositionCommandExecute);
        private bool CanUpSetPositionCommandExecute(object p) => SelectPosition is { };
        private void OnUpSetPositionCommandExecuted(object p)
        {
            SelectPosition.SetPosition += 1;
        }

        private ICommand _DownSetPositionCommand;
        /// <summary> Увеличить позицию задания на еденицу </summary>
        public ICommand DownSetPositionCommand => _DownSetPositionCommand ??=
            new LambdaCommand(OnDownSetPositionCommandExecuted, CanDownSetPositionCommandExecute);
        private bool CanDownSetPositionCommandExecute(object p) => SelectPosition is { };
        private void OnDownSetPositionCommandExecuted(object p)
        {
            SelectPosition.SetPosition -= 1;
        }

        private ICommand _WriteValueToControllerCommand;
        /// <summary> Записать значение в контроллер </summary>
        public ICommand WriteValueToControllerCommand => _WriteValueToControllerCommand ??=
            new LambdaCommand(OnWriteValueToControllerCommandExecuted, CanWriteValueToControllerCommandExecute);
        private bool CanWriteValueToControllerCommandExecute(object p) => p is { };
        private void OnWriteValueToControllerCommandExecuted(object p)
        {
            _mixReaderService.SetCurrentPosition(out int error, 31, 30000);
        }

        #region Поддержка

        private ICommand _CloseApplicationCommand;
        /// <summary> Закрыть приложение </summary>
        public ICommand CloseApplicationCommand => _CloseApplicationCommand ??=
            new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion Поддержка

        #endregion Команды

        #region Вспомогательные методы


        private int oldPos = -1;
        private int oldLeft = -1;
        private int oldRight = -1;
        private int fixTarget = -1;
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;

            try
            {
                var dataGetted = _mixReaderService.GetActualValues(out int error, out int pos, out int target, out int left, out int right);
                if (dataGetted)
                {
                    CurrentPosition = pos;
                    if (target != 0)
                    {
                        TargetPlace = _repositoryService.GetPositions().SingleOrDefault(p => p.Target == target)?.Name ?? "Отдых";
                    }
                    else
                    {
                        TargetPlace = "Отдых";
                    }
                    FixerLeftFixed = left switch
                    {
                        1 => "Открыт",
                        2 => "Закрыт",
                        _ => "Промежуточное",
                    };
                    FixerRightFixed = right switch
                    {
                        1 => "Открыт",
                        2 => "Закрыт",
                        _ => "Промежуточное",
                    };
                    if (target == 400 || target == 2 || target == 300 || target == 18)
                    {
                        if ( (left == 2 && oldLeft == 0 && right == 2) || (right == 2 && oldRight == 0 && left == 2))
                        {
                            _repositoryService.SetFactPosition(target, oldPos);
                        }
                    }
                    else if (target != 0)
                    {
                        if ((left == 2 && oldLeft == 0 && right == 2) || (right == 2 && oldRight == 0 && left == 2))
                        {
                            fixTarget = target;
                        }
                        if (left == 2 && right == 2 && fixTarget == target)
                        {
                            _repositoryService.SetFactPosition(target, pos);
                        }
                    }

                    oldPos = pos;
                    oldLeft = left;
                    oldRight = right;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения контроллера заливки, ошибка: {ex.Message}");
                throw;
            }

            _timer.Enabled = true;
        }

        private void LoadData()
        {
            Positions.Clear();
            foreach (var item in _repositoryService.GetPositions())
                Positions.Add(item);
        }



        #endregion
    }
}
