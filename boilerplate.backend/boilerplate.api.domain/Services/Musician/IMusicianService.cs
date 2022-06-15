using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public interface IMusicianService : IBaseService
    {
        Task<EntriesQueryResponse<AllMusiciansReportModel>> GetAllMusicians();
    }
}