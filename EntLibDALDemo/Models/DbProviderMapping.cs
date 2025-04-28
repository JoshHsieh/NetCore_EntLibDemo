namespace EntLibDALDemo.Models
{
    public class DatabaseProviders
    {
        public const string SectionName = "DatabaseProviders";

        public string? MySqlProvider { get; set; }

        public string? PgSqlProvider { get; set; }
    }
}
