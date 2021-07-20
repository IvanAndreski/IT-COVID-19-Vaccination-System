using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models
{
    public class ChangeInVaccinesNewsModel
    {
        [Key]
        public DateTime Date { get; set; }

        public int AstraZenecaChange { get; set; }
        public int SinopharmChange { get; set; }
        public int PfizerChange { get; set; }
        public int SputnikChange { get; set; }
    }
}