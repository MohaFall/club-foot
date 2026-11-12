using System.Configuration;

namespace GestionEquipeFoot
{
    // Single place that gives the MySQL connection string.
    // The value comes from db.config (ignored by Git), never from the code.
    public static class Database
    {
        public static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["FootDb"].ConnectionString;
    }
}
