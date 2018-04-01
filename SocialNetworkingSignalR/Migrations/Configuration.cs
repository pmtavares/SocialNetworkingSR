namespace SocialNetworkingSignalR.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SocialNetworkingSignalR.Models.Data.Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "SocialNetworkingSignalR.Models.Data.Db";
        }

        protected override void Seed(SocialNetworkingSignalR.Models.Data.Db context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
