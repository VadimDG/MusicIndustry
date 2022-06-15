namespace boilerplate.api.core.Models
{
    public class ContactDisplayReportModel
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneCell { get; init; }
        public string PhoneBusiness { get; init; }
        public bool Active { get; init; }
        public int Labels { get; init; }
        public int Musicians { get; init; }
        public int Platforms { get; init; }
    }
}
