namespace COVID_19_Vaccination_System.Migrations.AppointmentContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointmentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailOfUser = c.String(),
                        Date = c.DateTime(nullable: false),
                        NameOfVaccine = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VaccinationsAtDays",
                c => new
                    {
                        Day = c.DateTime(nullable: false),
                        NumVaccinationsAtDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Day);
            
            CreateTable(
                "dbo.VaccineModels",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        TimeBetweenDoses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VaccineModels");
            DropTable("dbo.VaccinationsAtDays");
            DropTable("dbo.AppointmentModels");
        }
    }
}
