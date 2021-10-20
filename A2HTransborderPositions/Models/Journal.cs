using A2HTransborderPositions.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2HTransborderPositions.Models
{
    /// <summary> Элемент журнала передвижений трансбордера </summary>
    public class Journal : Entity
    {
        private DateTime _dateTime;
        /// <summary> Дата время отметки </summary>
        public DateTime DateTime
        {
            get => _dateTime;
            set => Set(ref _dateTime, value);
        }

        private string _position;
        /// <summary> Название позиции трансбордера </summary>
        public string Position
        {
            get => _position;
            set => Set(ref _position, value);
        }

        private int _targetPosition;
        /// <summary> Текущая позиция - цель в контроллере </summary>
        public int TargetPosition
        {
            get => _targetPosition;
            set
            {
                Set(ref _targetPosition, value);
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

        /// <summary> Ошибка позиционирования </summary>
        public int ErrorPosition
        {
            get => (TargetPosition != 0 && FactPosition != 0) ? TargetPosition - FactPosition : 0;
        }
    }
}
