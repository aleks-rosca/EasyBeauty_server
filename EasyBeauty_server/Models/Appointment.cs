namespace EasyBeauty_server.Models
{
    using System;

    public class Appointment
    {
        public int AppointmentID { set; get; }

        public DateTime StartTime { set; get; }

        public DateTime EndTime { set; get; }

        public string Notes { set; get; }

        public Employee Employee { set; get; }

        public Service Service { set; get; }

        public Customer Customer { set; get; }
    }
}
