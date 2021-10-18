using A2HTransborderPositions.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2HTransborderPositions.Models
{
    public class Position : Entity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int CurrentPosition { get; set; }
        public int FactPosition { get; set; }
        public int SetPosition { get; set; }
    }
}
