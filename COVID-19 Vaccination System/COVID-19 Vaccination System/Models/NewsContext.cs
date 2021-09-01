using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models
{
    public class NewsContext : DbContext
    {
        //
        // Vaccinated people at day Table DbSet
        public DbSet<VaccinationsNewsModel> VaccinationsNews { get; set; }

        //
        // Vaccinated people at day Table DbSet
        public DbSet<ChangeInVaccinesNewsModel> ChangeInVaccinesNews { get; set; }

        public NewsContext() : base("DefaultConnection") { }

        public static NewsContext Create()
        {
            return new NewsContext();
        }
    }
}