namespace COVID_19_Vaccination_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Number : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Number", c => c.String());
            DropColumn("dbo.AspNetUsers", "AccountType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AccountType", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Number");
        }
    }
}
