﻿namespace EasyBeauty_server.Models
{
    using System;

    public class Appointment
    {
        public int ID { set; get; }

        public DateTime StartTime { set; get; }

        public DateTime EndTime { set; get; }

        public string Notes { set; get; }

        public int EmployeeID { set; get; }

        public int ServiceID { set; get; }

        public Customer Customer { set; get; }
    }
}
