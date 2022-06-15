using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace boilerplate.api.data.Models
{
    public class MusicLabel : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public ICollection<MusicLabelContact> MusicLabelContacts { get; set; } = new List<MusicLabelContact>();
    }

    public static class MusicLabelExtension
    {
        public const string TABLE_NAME = "MusicLabels";

        public static void DescribeMusicLabel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MusicLabel>(c =>
            {
                c.Property(p => p.Name).IsRequired(true).HasMaxLength(ValidationHelper.MusicLabel.NameMaxLength).HasDefaultValue("('')");
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.Property(p => p.DateModified).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
            });

            modelBuilder.Entity<MusicLabel>().ToTable(TABLE_NAME);
        }
    }
}
