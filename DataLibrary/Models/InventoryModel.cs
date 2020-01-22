using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class InventoryModel
    {
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public int BinID { get; set; }
        public int QTY { get; set; }
    }
}
