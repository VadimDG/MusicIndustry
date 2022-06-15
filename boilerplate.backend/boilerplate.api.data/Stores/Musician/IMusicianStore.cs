using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public interface IMusicianStore : IBaseStore
    {
        Task<EntriesQueryResponse<AllMusiciansReportModel>> GetAllMusiciansAsync();
    }
}