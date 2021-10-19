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

        public void SetFactPosition(int target, int value)
        {
            if (_positions.FirstOrDefault(p => p.Target == target) is { } pos)
            {
                pos.FactPosition = value;
            }
        }

        private static List<Position> GetInitPositions()
        {
            var positions = new List<Position>
            {
                new Position { Id = 1, Name = "406-е место приём", Target = 400, Order = 1 },
                new Position { Id = 2, Name = "2-е место", Target = 2, Order = 2 },
                new Position { Id = 3, Name = "3-е место", Target = 3, Order = 3 },
                new Position { Id = 4, Name = "4-е место", Target = 4, Order = 4 },
                new Position { Id = 5, Name = "5-е место", Target = 5, Order = 5 },
                new Position { Id = 6, Name = "6-е место", Target = 6, Order = 6 },
                new Position { Id = 7, Name = "8-е место", Target = 8, Order = 7 },
                new Position { Id = 8, Name = "1-й ударный стол", Target = 220, Order = 8 },
                new Position { Id = 9, Name = "10-е место", Target = 10, Order = 9 },
                new Position { Id = 10, Name = "2-й ударный стол", Target = 210, Order = 10 },
                new Position { Id = 11, Name = "12-е место", Target = 12, Order = 11 },
                new Position { Id = 12, Name = "14-е место", Target = 14, Order = 12 },
                new Position { Id = 13, Name = "15-е место", Target = 15, Order = 13 },
                new Position { Id = 14, Name = "16-е место", Target = 16, Order = 14 },
                new Position { Id = 15, Name = "301-е место выдача", Target = 300, Order = 15 },
                new Position { Id = 16, Name = "18-е место", Target = 18, Order = 16 },
                new Position { Id = 17, Name = "19-е место", Target = 19, Order = 17 },
                new Position { Id = 18, Name = "20-е место", Target = 20, Order = 18 },
                new Position { Id = 19, Name = "21-е место", Target = 21, Order = 19 },
                new Position { Id = 20, Name = "22-е место", Target = 22, Order = 20 },
                new Position { Id = 21, Name = "23-е место", Target = 23, Order = 21 },
                new Position { Id = 22, Name = "24-е место", Target = 24, Order = 22 },
                new Position { Id = 23, Name = "25-е место", Target = 25, Order = 23 },
                new Position { Id = 24, Name = "26-е место", Target = 26, Order = 24 },
                new Position { Id = 25, Name = "27-е место", Target = 27, Order = 25 },
                new Position { Id = 26, Name = "28-е место", Target = 28, Order = 26 },
                new Position { Id = 27, Name = "29-е место", Target = 29, Order = 27 },
                new Position { Id = 28, Name = "30-е место", Target = 30, Order = 28 },
                new Position { Id = 29, Name = "31-е место", Target = 31, Order = 29 },
                new Position { Id = 30, Name = "32-е место", Target = 32, Order = 30 },
                new Position { Id = 31, Name = "33-е место", Target = 33, Order = 31 },
                new Position { Id = 32, Name = "34-е место", Target = 34, Order = 32 },
            };
            return positions;
        }
    }
}
