using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public interface IMusicLabelStore : IBaseStore
    {
        Task<EntriesQueryResponse<AllMusicLabelsReportModel>> GetAllMusicLabels();
    }
}