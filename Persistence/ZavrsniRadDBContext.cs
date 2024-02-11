using Microsoft.EntityFrameworkCore;
using System.Reflection;
using zavrsni_backend.Entities;

namespace zavrsni_backend.Persistence
{
    public class ZavrsniRadDBContext : DbContext
    {
        public ZavrsniRadDBContext(DbContextOptions<ZavrsniRadDBContext> options) : base(options)  { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<RecordType> RecordTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
