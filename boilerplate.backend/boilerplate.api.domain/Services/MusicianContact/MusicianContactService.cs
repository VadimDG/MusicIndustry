using AutoMapper;
using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Query.MusicianContact;
using boilerplate.api.data.Models;
using boilerplate.api.data.Stores.MusicianContact;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services.PlatformContact
{
    public class MusicianContactService : BaseService, IMusicianContactService
    {
        public MusicianContactService(IMusicianContactStore store, ILogger<MusicianContactService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {
            
        }

        public async Task<EntriesQueryResponse<MusicianContactReportModel>> GetMusiciansByContactIdsAsync(List<int> contactIds)
        {
            string query = $@"SELECT * FROM {MusicianContactExtension.TABLE_NAME} 
                            WHERE {nameof(data.Models.MusicLabelContact.ContactId)} IN ({string.Join(',', contactIds)})";

            return await GetEntries<MusicianContactReportModel>(query);
        }
    }
}
