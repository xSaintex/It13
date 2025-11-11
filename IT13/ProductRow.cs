// ProductRow.cs
namespace IT13
{
    public class ProductRow
    {
        public string Name { get; set; } = "";
        public int Qty { get; set; } = 1;
        public decimal Price { get; set; } = 0m;
        public int Available { get; set; } = 0;
    }
}