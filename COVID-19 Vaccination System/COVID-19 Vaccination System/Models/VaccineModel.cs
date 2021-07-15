using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models {
    public class VaccineModel {
        [Key]
        public string Name { get; set; }

        public int TimeBetweenDoses { get; set; }
    }
}