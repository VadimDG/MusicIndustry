using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace boilerplate.api.data.Models
{
    public class Contact : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string PhoneCell { get; set; }
        public string PhoneBusiness { get; set; }
        public string Fax { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public ICollection<MusicianContact> MusicianContacts { get; set; } = new List<MusicianContact>();
        public ICollection<PlatformContact> PlatformContacts { get; set; } = new List<PlatformContact>();
        public ICollection<MusicLabelContact> MusicLabelContacts { get; set; } = new List<MusicLabelContact>();
    }

    public static class ContactExtension
    {
        public const string TABLE_NAME = "Contacts";

        public static void DescribeContact(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(c =>
            {
                c.Property(p => p.FirstName).IsRequired(true).HasMaxLength(ValidationHelper.Contact.NameMaxLength).HasDefaultValue("('')");
                c.Property(p => p.LastName).IsRequired(true).HasMaxLength(ValidationHelper.Contact.NameMaxLength).HasDefaultValue("('')");
                c.Property(p => p.Title).IsRequired(true).HasMaxLength(ValidationHelper.Contact.TitleMaxLength).HasDefaultValue("('')");
                c.Property(p => p.Company).IsRequired(true).HasMaxLength(ValidationHelper.Contact.CompanyMaxLength).HasDefaultValue("('')");
                c.Property(p => p.Email).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FiftyElementsLength).HasDefaultValue("('')");
                c.Property(p => p.PhoneCell).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FiftyElementsLength).HasDefaultValue("('')");
                c.Property(p => p.PhoneBusiness).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FiftyElementsLength).HasDefaultValue("('')");
                c.Property(p => p.Fax).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FiftyElementsLength).HasDefaultValue("('')");
                c.Property(p => p.AddressLine1).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FiftyElementsLength).HasDefaultValue("('')");
                c.Property(p => p.AddressLine2).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FiftyElementsLength).HasDefaultValue("('')");
                c.Property(p => p.City).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FiftyElementsLength).HasDefaultValue("('')");
                c.Property(p => p.State).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FiftyElementsLength).HasDefaultValue("('')");
                c.Property(p => p.Zip).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FiftyElementsLength).HasDefaultValue("('')");
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.Property(p => p.DateModified).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
            });

            modelBuilder.Entity<Contact>().ToTable(TABLE_NAME);
        }
    }
}
