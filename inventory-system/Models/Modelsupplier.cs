using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_system.Models
{
    internal class Modelsupplier : connection_db
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierAddress { get; set; }

    }
}
