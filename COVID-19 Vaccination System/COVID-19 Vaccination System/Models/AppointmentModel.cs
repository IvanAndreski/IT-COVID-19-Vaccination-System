using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models {
    public class AppointmentModel {
        [EmailAddress]
        public string EmailOfUser { get; set; }

        [Key]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Име на вакцина")]
        public string NameOfVaccine { get; set; }

        public int AppointmentNum { get; set; }

        public bool Confirmed { get; set; }
    }
}