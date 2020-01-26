namespace DataLibrary.Models
{
    public class OrderLinesModel
    {
        public int OrderLineID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int QTY { get; set; }
    }
}
