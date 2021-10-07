namespace EasyBeauty_server.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int ProductPrice { get; set; }

        public byte[] ProductImage { get; set; }
    }
}
