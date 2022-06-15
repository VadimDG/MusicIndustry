using System;

namespace boilerplate.api.core.Models.Query.MusicLabelContact
{
    public class MusicLabelContactReportModel
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int MusicLabelId { get; set; }
        public int ContactId { get; set; }
    }
}
