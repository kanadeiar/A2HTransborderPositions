using A2HTransborderPositions.Commands;
using A2HTransborderPositions.Commands.Base;
using System;
using System.Collections.Generic;
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



        #endregion Data

        #region Properties





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

        public MainWindowViewModel()
        {

        }

        #region Commands




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
    }
}
