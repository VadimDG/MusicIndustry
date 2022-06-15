using boilerplate.api.core.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace boilerplate.api.core.Models
{
    public record ContactCreateModel
    {
        [Required]
        [StringLength(ValidationHelper.Contact.NameMaxLength)]
        public string FirstName { get; init; }

        [Required]
        [StringLength(ValidationHelper.Contact.NameMaxLength)]
        public string LastName { get; init; }

        [StringLength(ValidationHelper.Contact.TitleMaxLength)]
        public string Title { get; init; }

        [StringLength(ValidationHelper.Contact.CompanyMaxLength)]
        public string Company { get; init; }

        [StringLength(ValidationHelper.Contact.EmailMaxLength)]
        public string Email { get; init; }

        [StringLength(ValidationHelper.Contact.FiftyElementsLength)]
        public string CellPhone { get; init; }

        [StringLength(ValidationHelper.Contact.FiftyElementsLength)]
        public string BusinessPhone { get; init; }

        [StringLength(ValidationHelper.Contact.FiftyElementsLength)]
        public string Fax { get; init; }

        [StringLength(ValidationHelper.Contact.FiftyElementsLength)]
        public string AddressLine1 { get; init; }

        [StringLength(ValidationHelper.Contact.FiftyElementsLength)]
        public string AddressLine2 { get; init; }

        [StringLength(ValidationHelper.Contact.FiftyElementsLength)]
        public string City { get; init; }

        [StringLength(ValidationHelper.Contact.FiftyElementsLength)]
        public string State { get; init; }
        
        [StringLength(ValidationHelper.Contact.FiftyElementsLength)]
        public string Zip { get; init; }

        public bool IsActive { get; init; }

        public ICollection<int> LabelIds { get; set; }

        public ICollection<int> MusicianIds { get; set; }

        public ICollection<int> PlatformIds { get; set; }
    }
}
