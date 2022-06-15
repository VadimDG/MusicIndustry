using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Query.MusicLabelContact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services.MusicLabelContact
{
    public interface IMusicLabelContactService : IBaseService
    {
        Task<EntriesQueryResponse<MusicLabelContactReportModel>> GetMusicLabelsByContactIdsAsync(List<int> contactIds);
    }
}
