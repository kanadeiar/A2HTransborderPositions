using A2HTransborderPositions.Models;
using System;
using System.Collections.Generic;

namespace A2HTransborderPositions.Interfaces
{
    public interface IJournalPositionsService
    {
        /// <summary> Получение данных журналов движения трансбордера </summary>
        public IEnumerable<Journal> GetJournals();
        /// <summary> Добавление новой записи в журнал </summary>
        public void AddToJournal(DateTime dateTime, string position, int targetPosition, int factPosition);
    }
}