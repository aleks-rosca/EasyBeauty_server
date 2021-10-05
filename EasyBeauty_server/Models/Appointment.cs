using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBeauty_server.Models
{
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
