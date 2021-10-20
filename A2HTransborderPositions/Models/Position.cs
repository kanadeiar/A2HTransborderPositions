using A2HTransborderPositions.Models.Base;
using A2HTransborderPositions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2HTransborderPositions.Models
{
    /// <summary> Позиция трансбордера </summary>
    public class Position : Entity
    {
        private string _name;
        /// <summary> Название позиции </summary>
        public string Name 
        { 
            get => _name; 
            set => Set(ref _name, value ); 
        }

        private int _target;
        /// <summary> Номер цель аппаратный </summary>
        public int Target 
        { 
            get => _target; 
            set => Set(ref _target, value ); 
        }

        private int _order;
        /// <summary> Сортировка </summary>
        public int Order 
        { 
            get => _order; 
            set => Set(ref _order, value ); 
        }

        private int _currentPosition;
        /// <summary> Текущая позиция в контроллере </summary>
        public int CurrentPosition
        {
            get => _currentPosition;
            set
            {
                Set(ref _currentPosition, value);
                OnPropertyChanged(nameof(ErrorPosition));
            }
        }

        private int _factPosition;
        /// <summary> Позиция по факту </summary>
        public int FactPosition
        {
            get => _factPosition;
            set
            {
                Set(ref _factPosition, value);
                OnPropertyChanged(nameof(ErrorPosition));
            }
        }

        private int _setPosition;
        /// <summary> Установка позиции </summary>
        public int SetPosition 
        { 
            get => _setPosition; 
            set => Set(ref _setPosition, value ); 
        }

        /// <summary> Ошибка позиционирования </summary>
        public int ErrorPosition
        {
            get => (CurrentPosition != 0 && FactPosition != 0) ? CurrentPosition - FactPosition : 0;
        }

    }
}
