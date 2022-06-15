namespace boilerplate.api.core.Models
{
    public record ContactUpdateModel: ContactCreateModel
    {
        public int Id { get; init; }
    }
}
