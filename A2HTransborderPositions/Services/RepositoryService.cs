using A2HTransborderPositions.Interfaces;
using A2HTransborderPositions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2HTransborderPositions.Services
{
    public class RepositoryService : IRepositoryService
    {
        private List<Position> _positions = GetInitPositions();

        public RepositoryService()
        {
            
        }

        public IEnumerable<Position> GetPositions() => _positions;


        public void SetCurrentPositions(int[] values)
        {
            for (int i = 0; i < _positions.Count; i++)
            {
                _positions[i].CurrentPosition = values[i];
            }
        }

        private static List<Position> GetInitPositions()
        {
            var positions = new List<Position>
            {
                new Position { Id = 1, Name = "406-е приём", Order = 1 },
                new Position { Id = 2, Name = "2-е место", Order = 2 },
                new Position { Id = 3, Name = "3-е место", Order = 3 },
                new Position { Id = 4, Name = "4-е место", Order = 4 },
                new Position { Id = 5, Name = "5-е место", Order = 5 },
                new Position { Id = 6, Name = "6-е место", Order = 6 },
                new Position { Id = 7, Name = "8-е место", Order = 7 },
                new Position { Id = 8, Name = "1-й ударный стол", Order = 8 },
                new Position { Id = 9, Name = "10-е место", Order = 9 },
                new Position { Id = 10, Name = "2-й ударный стол", Order = 10 },
                new Position { Id = 11, Name = "12-е место", Order = 11 },
                new Position { Id = 12, Name = "14-е место", Order = 12 },
                new Position { Id = 13, Name = "15-е место", Order = 13 },
                new Position { Id = 14, Name = "16-е место", Order = 14 },
                new Position { Id = 15, Name = "301-е выдача", Order = 15 },
                new Position { Id = 16, Name = "18-е место", Order = 16 },
                new Position { Id = 17, Name = "19-е место", Order = 17 },
                new Position { Id = 18, Name = "20-е место", Order = 18 },
                new Position { Id = 19, Name = "21-е место", Order = 19 },
                new Position { Id = 20, Name = "22-е место", Order = 20 },
                new Position { Id = 21, Name = "23-е место", Order = 21 },
                new Position { Id = 22, Name = "24-е место", Order = 22 },
                new Position { Id = 23, Name = "25-е место", Order = 23 },
                new Position { Id = 24, Name = "26-е место", Order = 24 },
                new Position { Id = 25, Name = "27-е место", Order = 25 },
                new Position { Id = 26, Name = "28-е место", Order = 26 },
                new Position { Id = 27, Name = "29-е место", Order = 27 },
                new Position { Id = 28, Name = "30-е место", Order = 28 },
                new Position { Id = 29, Name = "31-е место", Order = 29 },
                new Position { Id = 30, Name = "32-е место", Order = 30 },
                new Position { Id = 31, Name = "33-е место", Order = 31 },
                new Position { Id = 32, Name = "34-е место", Order = 32 },
            };
            return positions;
        }
    }
}
