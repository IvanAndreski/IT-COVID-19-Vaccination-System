namespace COVID_19_Vaccination_System.Migrations.AppointmentContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppointmentModels", "AppointmentNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppointmentModels", "AppointmentNum");
        }
    }
}
