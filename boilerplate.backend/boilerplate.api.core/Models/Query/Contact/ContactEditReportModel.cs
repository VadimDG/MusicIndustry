using System;
using System.Collections.Generic;

namespace boilerplate.api.core.Models
{
    public class ContactEditReportModel
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Title { get; init; }
        public string Company { get; init; }
        public string Email { get; init; }
        public string PhoneCell { get; init; }
        public string PhoneBusiness { get; init; }
        public string Fax { get; init; }
        public string AddressLine1 { get; init; }
        public string AddressLine2 { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Zip { get; init; }
        public bool IsActive { get; init; }
        public ICollection<int> MusicLabels { get; set; }
        public ICollection<int> Platforms { get; set; }
        public ICollection<int> Musicians { get; set; }
    }
}
