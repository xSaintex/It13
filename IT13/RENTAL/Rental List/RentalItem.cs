// RentalItem.cs
namespace IT13
{
    public class RentalItem
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal RentalPrice { get; set; }
        public int AvailableQty { get; set; }
        public decimal Subtotal => Quantity * RentalPrice;
    }
}