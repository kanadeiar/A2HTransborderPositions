using A2HTransborderPositions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2HTransborderPositions.Interfaces
{
    public interface IRepositoryService
    {
        public IEnumerable<Position> GetPositions();
        public void SetCurrentPositions(int[] values);
        public void SetFactPosition(int target, int value);
    }
}
