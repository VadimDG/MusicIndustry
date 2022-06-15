using System;

namespace boilerplate.api.core.Models.Query.PlatformContact
{
    public class PlatformContactReportModel
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int PlatformId { get; set; }
        public int ContactId { get; set; }
    }
}
