using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2HTransborderPositions.Interfaces
{
    public interface IMixReaderService
    {
        /// <summary> Тест соединения с контроллером </summary>
        public bool TestConnection(out int error);

        /// <summary> Получение данных текущих позиций </summary>
        public bool GetCurrentPositions(out int error, int[] values);

        /// <summary> Запись значения в блок данных </summary>
        public bool SetCurrentPosition(out int error, int index, int value);

        /// <summary> Получение текущих актуальных данных парома </summary>
        public bool GetActualValues(out int error, out int position, out int target, out int left, out int right);
    }
}
