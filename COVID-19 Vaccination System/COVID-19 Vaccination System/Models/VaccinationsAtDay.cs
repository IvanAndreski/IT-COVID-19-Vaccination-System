using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models {
    public class VaccinationsAtDay {
        [Key]
        public DateTime Day { get; set; }

        public int NumVaccinationsAtDay { get; set; }

        public VaccinationsAtDay() {
            NumVaccinationsAtDay = 0;
        }
    }
}