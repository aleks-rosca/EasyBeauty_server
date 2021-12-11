namespace EasyBeauty_server.Models
{
    public class Employee : Customer
    {
        public int ID { get; set; }

        public new string Email { get; set; }

        public new string Name { get; set; }

        public new int PhoneNr { get; set; }

        public string Role { get; set; }
    }
}
