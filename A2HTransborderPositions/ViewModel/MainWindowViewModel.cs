using A2HTransborderPositions.Commands;
using A2HTransborderPositions.Commands.Base;
using A2HTransborderPositions.Interfaces;
using A2HTransborderPositions.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace A2HTransborderPositions.ViewModel
{
    class MainWindowViewModel : Base.ViewModel
    {
        #region Data

        private readonly IRepositoryService _repositoryService;
        private readonly IMixReaderService _mixReaaderService;

        #endregion Data

        #region Properties

        public ObservableCollection<Position> Positions { get; } = new ();



        #region Support

        private string _Title = "A2H Утилита настройки позиций трансбордера";
        /// <summary> Заголовок приложения </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion Support

        #endregion Properties

        public MainWindowViewModel(IRepositoryService repositoryService, IMixReaderService mixReaaderService)
        {
            _repositoryService = repositoryService;
            _mixReaaderService = mixReaaderService;

            LoadData();
        }

        #region Commands

        private ICommand _UpdateApplicationCommand;


        /// <summary> Обновить данные приложения </summary>
        public ICommand UpdateApplicationCommand => _UpdateApplicationCommand ??=
            new LambdaCommand(OnUpdateApplicationCommandExecuted, CanUpdateApplicationCommandExecute);

        private bool CanUpdateApplicationCommandExecute(object p) => true;

        private void OnUpdateApplicationCommandExecuted(object p)
        {
            var values = new int[32];
            _mixReaaderService.GetCurrentPositions(out int error, values);
            _repositoryService.SetCurrentPositions(values);
            LoadData();
        }


        private ICommand _RunApplicationCommand;


        /// <summary> Обновить данные приложения </summary>
        public ICommand RunApplicationCommand => _RunApplicationCommand ??=
            new LambdaCommand(OnRunApplicationCommandExecuted, CanRunApplicationCommandExecute);

        private bool CanRunApplicationCommandExecute(object p) => true;

        private void OnRunApplicationCommandExecuted(object p)
        {
            _mixReaaderService.GetActualValues(out int error, out int pos, out bool left, out bool right);
        }

        #region Support

        private ICommand _CloseApplicationCommand;


        /// <summary> Закрыть приложение </summary>
        public ICommand CloseApplicationCommand => _CloseApplicationCommand ??=
            new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion Support

        #endregion Commands

        #region Вспомогательные методы

        private void LoadData()
        {
            Positions.Clear();
            foreach (var item in _repositoryService.GetPositions())
                Positions.Add(item);
        }

        #endregion
    }
}
