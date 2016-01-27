namespace MirrorWebService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MirrorWebService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MirrorWebService.Models.NoteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MirrorWebService.Models.NoteContext";
        }

        protected override void Seed(MirrorWebService.Models.NoteContext context)
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
            //TODO: I removed seed data bc it was annoying
            //context.Notes.AddOrUpdate(x => x.Id,
            //    new Note() { Id = 1, NoteText = "Love is to you!" },
            //    new Note() { Id = 2, NoteText = "Bro Fist~" });
        }
    }
}
