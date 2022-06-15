using System.ComponentModel.DataAnnotations;

namespace boilerplate.api.core.Models.Command.PlatformContact
{
    public record PlatformContactCreateModel
    {
        [Required]
        public int PlatformId { get; init; }
        [Required]
        public int ContactId { get; set; }
    }
}
