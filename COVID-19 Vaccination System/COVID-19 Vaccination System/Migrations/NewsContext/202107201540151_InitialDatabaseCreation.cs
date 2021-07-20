namespace COVID_19_Vaccination_System.Migrations.NewsContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ChangeInVaccinesNewsModels");
            AddColumn("dbo.ChangeInVaccinesNewsModels", "Date", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.ChangeInVaccinesNewsModels", "Date");
            DropColumn("dbo.ChangeInVaccinesNewsModels", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChangeInVaccinesNewsModels", "MyProperty", c => c.DateTime(nullable: false));
            DropPrimaryKey("dbo.ChangeInVaccinesNewsModels");
            DropColumn("dbo.ChangeInVaccinesNewsModels", "Date");
            AddPrimaryKey("dbo.ChangeInVaccinesNewsModels", "MyProperty");
        }
    }
}
