using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Query.MusicianContact;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace boilerplate.api.domain.Services.PlatformContact
{
    public interface IMusicianContactService : IBaseService
    {
        Task<EntriesQueryResponse<MusicianContactReportModel>> GetMusiciansByContactIdsAsync(List<int> contactIds);
    }
}
