using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecordType = zavrsni_backend.Entities.RecordType;

namespace zavrsni_backend.Persistence.Configurations
{
    public class RecordTypeConfiguration : IEntityTypeConfiguration<RecordType>
    {
        public void Configure(EntityTypeBuilder<RecordType> builder)
        {
            builder.ToTable("RecordTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();

            builder.HasData(
                new { Id = (int)Enums.RecordType.Whitelisted, Name = Enum.GetName(Enums.RecordType.Whitelisted) },
                new { Id = (int)Enums.RecordType.Blacklisted, Name = Enum.GetName(Enums.RecordType.Blacklisted) },
                new { Id = (int)Enums.RecordType.Unclassified, Name = Enum.GetName(Enums.RecordType.Unclassified) }
            );
        }
    }
}