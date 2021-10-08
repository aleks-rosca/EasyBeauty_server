namespace EasyBeauty_server.Models
{
    using System;

    public class AppointmentDB
    {
        public int ID { set; get; }

        public DateTime StartTime { set; get; }

        public DateTime EndTime { set; get; }

        public string Notes { set; get; }

        public int EmployeeID { set; get; }

        public int ServiceID { set; get; }

        public int PhoneNr { set; get; }

        public bool IsAccepted { set; get; }
    }
}
