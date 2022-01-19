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
        private readonly IJournalPositionsService _journalPositionsService;
        private Timer _timer;

        #endregion Данные

        #region Свойства

        /// <summary> Позиции трансбордера </summary>
        public ObservableCollection<Position> Positions { get; } = new ();

        private Position _selectPosition;
        /// <summary> Выбранная в списке текущая позиция трансбордера </summary>
        public Position SelectPosition
        {
            get => _selectPosition;
            set
            {
                Set(ref _selectPosition, value);
                OnPropertyChanged(nameof(AddressOfDataBlock));
                OnPropertyChanged(nameof(SelectPositionJournals));
            }
        }
        /// <summary> Адрес блока данных </summary>
        public string AddressOfDataBlock
        {
            get => $"DB{Sharp7MixReaderService.GetNumberBlockFromIndex(Positions.IndexOf(SelectPosition))}.DBD12";
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

        /// <summary> Журналы передвижений трансбордера </summary>
        public IEnumerable<Journal> Journals => _journalPositionsService.GetJournals();

        /// <summary> Журнылы передвижений отфильтрованный для выбранной позиции </summary>
        public IEnumerable<Journal> SelectPositionJournals => _journalPositionsService.GetJournals().Where(j => j.Position == SelectPosition.Name);

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

        public MainWindowViewModel(IRepositoryService repositoryService, IMixReaderService mixReaaderService, IJournalPositionsService journalPositionsService)
        {
            _repositoryService = repositoryService;
            _mixReaderService = mixReaaderService;
            _journalPositionsService = journalPositionsService;
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
            if (SelectPosition is null) return;
            if (MessageBox.Show("Действительно записать это значение в программируемый логический контроллер?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
                return;
            _mixReaderService.SetCurrentPosition(out int error, Positions.IndexOf(SelectPosition), SelectPosition.SetPosition);
        }

        private ICommand _UpdateJournalCommand;
        /// <summary> Обновить журнал движений трансбордера </summary>
        public ICommand UpdateJournalCommand => _UpdateJournalCommand ??=
            new LambdaCommand(OnUpdateJournalCommandExecuted, CanUpdateJournalCommandExecute);
        private bool CanUpdateJournalCommandExecute(object p) => true;
        private void OnUpdateJournalCommandExecuted(object p)
        {
            OnPropertyChanged(nameof(Journals));
        }

        private ICommand _UpdateSelectJournalCommand;
        /// <summary> Обновить журнал движений трансбордера </summary>
        public ICommand UpdateSelectJournalCommand => _UpdateSelectJournalCommand ??=
            new LambdaCommand(OnUpdateSelectJournalCommandExecuted, CanUpdateSelectJournalCommandExecute);
        private bool CanUpdateSelectJournalCommandExecute(object p) => true;
        private void OnUpdateSelectJournalCommandExecuted(object p)
        {
            OnPropertyChanged(nameof(SelectPositionJournals));
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
        private Journal fixJournal = null;
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

                            _journalPositionsService.AddToJournal(DateTime.Now, TargetPlace, _repositoryService.GetPositions().SingleOrDefault(p => p.Target == target)?.CurrentPosition ?? 0, oldPos);

                            //Journals.Add(new Journal
                            //{
                            //    DateTime = DateTime.Now,
                            //    Position = TargetPlace,
                            //    TargetPosition = _repositoryService.GetPositions().SingleOrDefault(p => p.Target == target)?.CurrentPosition ?? 0,
                            //    FactPosition = oldPos,
                            //});
                        }
                    }
                    else if (target != 0)
                    {
                        if ((left == 2 && oldLeft == 0 && right == 2) || (right == 2 && oldRight == 0 && left == 2))
                        {
                            fixTarget = target;

                            _journalPositionsService.AddToJournal(DateTime.Now, TargetPlace, _repositoryService.GetPositions().SingleOrDefault(p => p.Target == target)?.CurrentPosition ?? 0, pos);

                            //Journals.Add(new Journal
                            //{
                            //    DateTime = DateTime.Now,
                            //    Position = TargetPlace,
                            //    TargetPosition = _repositoryService.GetPositions().SingleOrDefault(p => p.Target == target)?.CurrentPosition ?? 0,
                            //    FactPosition = pos,
                            //});
                        }
                        if (left == 2 && right == 2 && fixTarget == target)
                        {
                            _repositoryService.SetFactPosition(target, pos);

                            //Journals.LastOrDefault().FactPosition = pos;
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
