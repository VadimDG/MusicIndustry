using AutoMapper;
using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Query.MusicLabelContact;
using boilerplate.api.data.Models;
using boilerplate.api.data.Stores.LabelContact;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services.MusicLabelContact
{
    public class MusicLabelContactService : BaseService, IMusicLabelContactService
    {
        public MusicLabelContactService(IMusicLabelContactStore store, ILogger<MusicLabelContactService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {

        }

        public async Task<EntriesQueryResponse<MusicLabelContactReportModel>> GetMusicLabelsByContactIdsAsync(List<int> contactIds)
        {
            string query = $@"SELECT * FROM {MusicLabelContactExtension.TABLE_NAME} 
                            WHERE {nameof(data.Models.MusicLabelContact.ContactId)} IN ({string.Join(',', contactIds)})";

            return await GetEntries<MusicLabelContactReportModel>(query);
        }
    }
}
