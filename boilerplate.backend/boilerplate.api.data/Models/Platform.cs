using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace boilerplate.api.data.Models
{
    public class Platform : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public ICollection<PlatformContact> PlatformContacts { get; set; } = new List<PlatformContact>();
    }

    public static class PlatformExtension
    {
        public const string TABLE_NAME = "Platforms";

        public static void DescribePlatform(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>(c =>
            {
                c.Property(p => p.Name).IsRequired(true).HasMaxLength(ValidationHelper.Platform.NameMaxLength).HasDefaultValue("('')");
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.Property(p => p.DateModified).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
            });

            modelBuilder.Entity<Platform>().ToTable(TABLE_NAME);
        }
    }
}
