using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models {
    public class AppointmentModel {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        public string EmailOfUser { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public string NameOfVaccine { get; set; }
    }
}