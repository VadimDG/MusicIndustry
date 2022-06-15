using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Command.MusicLabelContact;
using boilerplate.api.core.Models.Query.MusicLabelContact;
using Refit;
using System.Threading.Tasks;

namespace boilerplate.api.core.Clients
{
    public interface IMusicLabelContactClient
    {
        [Get(RoutesHelper.MusicLabelContact.Id)]
        Task<EntriesQueryResponse<MusicLabelContactReportModel>> GetEntries(int id);

        [Post(RoutesHelper.MusicLabelContact.Base)]
        Task<CreateCommandResponse<int>> CreateEntry([Body] CreateCommandRequest<MusicLabelContactCreateModel> request);

        [Put(RoutesHelper.MusicLabelContact.Id)]
        Task<UpdateCommandResponse> UpdateEntry(int id, [Body] UpdateCommandRequest<MusicLabelContactUpdateModel> request);

        [Delete(RoutesHelper.MusicLabelContact.Id)]
        Task<DeleteCommandResponse> DeleteEntry(int id);
    }
}
