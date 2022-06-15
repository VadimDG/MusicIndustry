using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Query.PlatformContact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services.PlatformContact
{
    public interface IPlatformContactService : IBaseService
    {
        Task<EntriesQueryResponse<PlatformContactReportModel>> GetPlatformsByContactIdsAsync(List<int> contactIds);
    }
}
