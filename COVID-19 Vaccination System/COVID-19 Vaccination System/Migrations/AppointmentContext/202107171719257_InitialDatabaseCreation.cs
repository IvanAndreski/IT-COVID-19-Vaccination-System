namespace COVID_19_Vaccination_System.Migrations.AppointmentContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AppointmentModels");
            AlterColumn("dbo.AppointmentModels", "EmailOfUser", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AppointmentModels", "EmailOfUser");
            DropColumn("dbo.AppointmentModels", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppointmentModels", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.AppointmentModels");
            AlterColumn("dbo.AppointmentModels", "EmailOfUser", c => c.String());
            AddPrimaryKey("dbo.AppointmentModels", "Id");
        }
    }
}
