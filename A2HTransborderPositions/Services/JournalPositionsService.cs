using A2HTransborderPositions.Interfaces;
using A2HTransborderPositions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2HTransborderPositions.Services
{
    public class JournalPositionsService : IJournalPositionsService
    {
        private List<Journal> _journals = new ();

        public JournalPositionsService()
        {

        }

        public IEnumerable<Journal> GetJournals() => _journals.OrderByDescending(j => j.DateTime).ToList();

        public void AddToJournal(DateTime dateTime, string position, int targetPosition, int factPosition)
        {
            _journals.Add(new Journal
            {
                DateTime = dateTime,
                Position = position,
                TargetPosition = targetPosition,
                FactPosition = factPosition,
            });
        }
    }
}
