using boilerplate.api.core.Models;
using System.Collections.Generic;

namespace boilerplate.ui.Models
{
    public class ContactUpdateEntryViewModel
    {
        public FormModel Form { get; set; }
        public record FormModel : ContactUpdateModel
        {            
            public List<Relationship> ExistingPlatforms { get; set; }
            public List<Relationship> ExistingLabels { get; set; }
            public List<Relationship> ExistingMusicians { get; set; }
            public string Labels { get; set; }
            public string Musicians { get; set; }
            public string Platforms { get; set; }
        }

        public List<Relationship> Relationships { get; set; }
        public string ErrorMessage { get; set; }
    }
}
