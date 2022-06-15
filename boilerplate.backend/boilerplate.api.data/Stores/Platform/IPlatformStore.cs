using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public interface IPlatformStore : IBaseStore
    {
        Task<EntriesQueryResponse<AllPlatformsReportModel>> GetAllPlatforms();
    }
}