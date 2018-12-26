namespace Infrastructure
{
    public static class Configuration
    {
        public static string ConnectionString 
            => "Server=(localdb)\\mssqllocaldb;Database=ExpensesBotDb;Trusted_Connection=True;";
    }
}
