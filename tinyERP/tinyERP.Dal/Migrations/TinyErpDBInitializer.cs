using System.Data.Entity;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Migrations
{
    class TinyErpDBInitializer : DropCreateDatabaseIfModelChanges<TinyErpContext>
    {
        protected override void Seed(TinyErpContext context)
        {
            Category c1 = context.Categories.Add(new Category() { Name = "Büro"});
            context.Categories.Add(new Category() { Name = "Schreibmaterial", ParentCategory = c1 });
            context.Categories.Add(new Category() { Name = "Druckmaterial", ParentCategory = c1 });
            Category c2 = context.Categories.Add(new Category() { Name = "Auto" });
            context.Categories.Add(new Category() { Name = "Service", ParentCategory = c2 });
            context.Categories.Add(new Category() { Name = "Treibstoff", ParentCategory = c2 });
            context.Categories.Add(new Category() { Name = "Versicherung", ParentCategory = c2 });
            Category c3 = context.Categories.Add(new Category() { Name = "Infrastruktur" });
            context.Categories.Add(new Category() { Name = "Miete", ParentCategory = c3 });
            context.Categories.Add(new Category() { Name = "Versicherung", ParentCategory = c3 });
            context.Categories.Add(new Category() { Name = "Einrichtung", ParentCategory = c3 });
            Category c4 = context.Categories.Add(new Category() { Name = "Kommunikation" });
            context.Categories.Add(new Category() { Name = "Mobile", ParentCategory = c4 });
            context.Categories.Add(new Category() { Name = "Festnetz", ParentCategory = c4 });
            context.Categories.Add(new Category() { Name = "Internet", ParentCategory = c4 });
            Category c5 = context.Categories.Add(new Category() { Name = "Abgaben" });
            context.Categories.Add(new Category() { Name = "Steuern", ParentCategory = c5 });
            context.Categories.Add(new Category() { Name = "Sozialabgaben (AHV..)", ParentCategory = c5 });
            context.Categories.Add(new Category() { Name = "Pensionskasse", ParentCategory = c5 });
            Category c6 = context.Categories.Add(new Category() { Name = "Auftrag" });
            context.Categories.Add(new Category() { Name = "Dienstleistungen", ParentCategory = c6 });
            context.Categories.Add(new Category() { Name = "Warenertrag", ParentCategory = c6 });
            context.Categories.Add(new Category() { Name = "Lizenzeinkünfte", ParentCategory = c6 });

            context.Categories.Add(new Category() { Name = "Weiterbildung" });
            context.Categories.Add(new Category() { Name = "Marketing/Werbung" });
            context.Categories.Add(new Category() { Name = "Wareneinkauf" });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
