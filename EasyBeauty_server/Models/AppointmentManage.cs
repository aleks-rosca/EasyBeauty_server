using System;

namespace EasyBeauty_server.Models
{
    public class AppointmentManage
    {
        public int ID { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
        public string Notes { set; get; }
        public int EmployeeID { set; get; }
        public string EmployeeName { set; get; }
        public int ServiceID { set; get; }
        public string ServiceName { set; get; }
        public double ServicePrice { set; get; }
        public int ServiceDuration { set; get; }

        public string CustomerName { set; get; }
        public int PhoneNr { set; get; }
        public string CustomerEmail { set; get; }
        public bool IsAccepted { set; get; }
    }
}