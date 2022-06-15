using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public interface IPlatformService : IBaseService
    {
        Task<EntriesQueryResponse<AllPlatformsReportModel>> GetAllPlatforms();
    }
}