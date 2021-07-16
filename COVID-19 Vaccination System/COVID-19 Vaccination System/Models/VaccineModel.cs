using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models {
    public class VaccineModel {
        [Key]
        [Display(Name = "Име на вакцина")]
        public string Name { get; set; }

        [Display(Name = "Достапни дози")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Внесовте недозволена вредност!")]
        public int NumOfDosesAvailable { get; set; }

        [Display(Name = "Денови помеѓу две дози")]
        public int TimeBetweenDoses { get; set; }
    }
}