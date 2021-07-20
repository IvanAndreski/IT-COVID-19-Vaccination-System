using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models
{
    public class VaccineEditViewModel
    {
        public VaccineModel Vaccine { get; set; }

        [Required(ErrorMessage = "Ова поле е задолжително!")]
        [Display(Name = "Број на нови дози")]
        public int NewDoses { get; set; }
    }
}