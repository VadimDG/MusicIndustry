using System.ComponentModel.DataAnnotations;

namespace boilerplate.api.core.Models.Command.MusicLabelContact
{
    public record MusicLabelContactCreateModel
    {
        [Required]
        public int MusicLabelId { get; set; }
        [Required]
        public int ContactId { get; set; }
    }
}
