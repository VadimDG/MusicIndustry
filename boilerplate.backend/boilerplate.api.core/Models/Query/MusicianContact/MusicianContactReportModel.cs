using System;

namespace boilerplate.api.core.Models.Query.MusicianContact
{
    public class MusicianContactReportModel
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int MusicianId { get; set; }
        public int ContactId { get; set; }
    }
}
