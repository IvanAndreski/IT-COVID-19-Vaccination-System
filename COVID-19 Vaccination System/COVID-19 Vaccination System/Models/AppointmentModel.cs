using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models {
    public class AppointmentModel {
        public UserModel User { get; set; }
        public DateTime Date { get; set; }
        public VaccineModel VaccineType { get; set; }
    }
}