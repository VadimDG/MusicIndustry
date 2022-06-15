using AutoMapper;
using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Query.PlatformContact;
using boilerplate.api.data.Models;
using boilerplate.api.data.Stores.PlatformContact;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services.PlatformContact
{
    public class PlatformContactService : BaseService, IPlatformContactService
    {
        public PlatformContactService(IPlatformContactStore store, ILogger<PlatformContactService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {
            
        }

        public async Task<EntriesQueryResponse<PlatformContactReportModel>> GetPlatformsByContactIdsAsync(List<int> contactIds)
        {
            string query = $@"SELECT * FROM {PlatformContactExtension.TABLE_NAME} 
                            WHERE {nameof(data.Models.PlatformContact.ContactId)} IN ({string.Join(',', contactIds)})";

            return await GetEntries<PlatformContactReportModel>(query);
        }
    }
}
