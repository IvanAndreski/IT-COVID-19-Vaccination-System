using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models
{
    public class NewsViewModel
    {
        public List<ChangeInVaccinesNewsModel> ChangeInVaccinesNewsList { get; set; }
        public List<VaccinationsNewsModel> VaccinationsNewsList { get; set; }
        public List<VaccineModel> AvailableVaccineList { get; set; }
    }
}