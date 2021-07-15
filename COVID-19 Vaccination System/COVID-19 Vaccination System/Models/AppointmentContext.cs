using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models {
    public class AppointmentContext : DbContext {
        //
        // Appointment Table DbSet
        public DbSet<AppointmentModel> Appointments { get; set; }

        //
        // Vaccinations at Day Table DbSet
        public DbSet<VaccinationsAtDay> VaccinationsAtDay { get; set; }

        //
        // Days between doses of each vaccine table
        public DbSet<VaccineModel> VaccineDaysBetweenDoses { get; set; }

        public AppointmentContext() : base("DefaultConnection") { }

        public static AppointmentContext Create() {
            return new AppointmentContext();
        }
    }
}