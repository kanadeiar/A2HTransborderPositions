using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrancborderTimeCalc.Model
{
    /// <summary> Эффективность по массивам </summary>
    public class Effective : Entity
    {
        private int _MassifNumber;
        /// <summary> Номер места куда или откуда делается действие </summary>
        public int MassifNumber
        {
            get => _MassifNumber;
            set => Set(ref _MassifNumber, value);
        }
        private int _Seconds;
        /// <summary> Количество секунд на это действие </summary>
        public int Seconds
        {
            get => _Seconds;
            set => Set(ref _Seconds, value);
        }        
    }
}
