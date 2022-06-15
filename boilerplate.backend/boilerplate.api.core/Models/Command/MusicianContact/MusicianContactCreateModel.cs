using System.ComponentModel.DataAnnotations;

namespace boilerplate.api.core.Models.Command.MusicianContact
{
    public record MusicianContactCreateModel
    {
        [Required]
        public int MusicianId { get; set; }
        [Required]
        public int ContactId { get; set; }
    }
}
