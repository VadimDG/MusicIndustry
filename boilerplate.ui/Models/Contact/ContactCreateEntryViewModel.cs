using boilerplate.api.core.Models;
using System.Collections.Generic;

namespace boilerplate.ui.Models
{
    public class ContactCreateEntryViewModel
    {
        public FormModel Form { get; set; }
        public record FormModel : ContactCreateModel
        {
            public List<Relationship> ExistingPlatforms { get; set; } = new List<Relationship>();
            public List<Relationship> ExistingLabels { get; set; } = new List<Relationship>();
            public List<Relationship> ExistingMusicians { get; set; } = new List<Relationship>();
            public string Labels { get; set; } = "";
            public string Musicians { get; set; } = "";
            public string Platforms { get; set; } = "";
        }

        public List<Relationship> Relationships { get; set; }
        public string ErrorMessage { get; set; }
    }
}
