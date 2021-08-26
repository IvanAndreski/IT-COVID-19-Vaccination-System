using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models
{
    public class CreateAppointmentViewModel
    {
        public List<VaccineModel> VaccineList { get; set; }
        public List<String> AvailableVaccineNameList { get; set; }
        public bool CanAppoint { get; set; }
    }
}