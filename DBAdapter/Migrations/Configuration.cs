namespace KMA.APZRPMJ2018.NumberConverter.DBAdapter.Migrations
{
    using System.Data.Entity.Migrations;
   
    internal sealed class Configuration : DbMigrationsConfiguration<ConversionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ConversionDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
