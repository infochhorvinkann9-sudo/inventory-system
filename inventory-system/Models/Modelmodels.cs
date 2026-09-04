using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_system.Models
{
    internal class Modelmodels: connection_db
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int ModelStatus { get; set; }
    }
}
