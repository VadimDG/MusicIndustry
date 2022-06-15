using boilerplate.api.data.Models;
using Microsoft.EntityFrameworkCore;

namespace boilerplate.api.data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MusicLabel> MusicLabels { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<MusicianContact> MusicianContacts { get; set; }
        public DbSet<MusicLabelContact> MusicLabelContacts { get; set; }
        public DbSet<PlatformContact> PlatformContacts { get; set; }
        public DbSet<ProcedureVersion> ProcedureVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(modelBuilder);

            modelBuilder.DescribeMusicLabel();
            modelBuilder.DescribeMusician();
            modelBuilder.DescribePlatform();
            modelBuilder.DescribeProcedureVersion();
            modelBuilder.DescribeContact();
            modelBuilder.DescribeMusicianContact();
            modelBuilder.DescribeMusicLabelContact();
            modelBuilder.DescribePlatformContact();
        }
    }
}
