namespace MirrorWebService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MirrorWebService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MirrorWebService.Models.CalendarSettingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MirrorWebService.Models.CalendarSettingContext context)
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

            context.CalendarSettings.AddOrUpdate(x => x.Id,
                new CalendarSetting() { Id = 1, ActiveCalendarAccount = "primary", CalendarOwnerAccount = "scottroot2@gmail.com", RefreshInterval = new TimeSpan(0, 0, 30) });
        }
    }
}
