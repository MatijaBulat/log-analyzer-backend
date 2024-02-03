using Microsoft.EntityFrameworkCore.Metadata.Builders;
using zavrsni_backend.Entities.Common;

namespace zavrsni_backend.Persistence.Configurations.Extensions
{
    public static class BuilderExtensions
    {
        public static void SetupEntity<T>(this EntityTypeBuilder<T> builder) where T : Entity
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
