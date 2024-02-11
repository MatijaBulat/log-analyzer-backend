using zavrsni_backend.Helpers;

namespace zavrsni_backend.Persistence
{
    public static class RecordsInitializer
    {
        private static readonly string whitelistFileName = ".\\Whitelist\\D8 VLAN217-2023-10-24-0100_2137.json";

        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ZavrsniRadDBContext>();

                context.Database.EnsureCreated();

                var records = context.Records.FirstOrDefault();

                if (records is null)
                {
                    using (var reader = new StreamReader(whitelistFileName))
                    {
                        string json = reader.ReadToEnd();

                        var parsedRecords = new LogFileParser(context).ParseItems(json);

                        context.Records.AddRange(parsedRecords);
                        
                        context.SaveChanges();
                    }
                }

                return app;
            }
        }
    }
}
