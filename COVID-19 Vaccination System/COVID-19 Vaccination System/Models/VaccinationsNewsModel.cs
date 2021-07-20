using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models
{
    public class VaccinationsNewsModel
    {
        [Key]
        public DateTime Day { get; set; }

        public int Vaccinations { get; set; }

    }
}