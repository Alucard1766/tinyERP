using System.Data.Entity.Migrations;

namespace tinyERP.Dal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TinyErpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = typeof(TinyErpContext).FullName;
        }
    }
}