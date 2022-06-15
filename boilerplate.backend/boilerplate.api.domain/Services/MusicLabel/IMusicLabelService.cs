using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public interface IMusicLabelService : IBaseService
    {
        Task<EntriesQueryResponse<AllMusicLabelsReportModel>> GetAllMusicLabels();
    }
}