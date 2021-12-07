namespace EasyBeauty_server.Models
{
    using System;

    public class Appointment
    {
        public int Id { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
        public string Notes { set; get; }
        public int EmployeeId { set; get; }
        public int ServiceId { set; get; }
        public string CustomerName { set; get; }
        public int PhoneNr { set; get; }
        public string CustomerEmail { set; get; }
        public bool IsAccepted { set; get; }
    }
}
