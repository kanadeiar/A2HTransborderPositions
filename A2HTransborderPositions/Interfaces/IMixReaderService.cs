﻿using System;
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
    }
}
