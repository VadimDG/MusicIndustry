using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Command.MusicianContact;
using boilerplate.api.core.Models.Query.MusicianContact;
using Refit;
using System.Threading.Tasks;

namespace boilerplate.api.core.Clients
{
    public interface IMusicianContactClient
    {
        [Get(RoutesHelper.MusicianContact.Id)]
        Task<EntriesQueryResponse<MusicianContactReportModel>> GetEntries(int id);

        [Post(RoutesHelper.MusicianContact.Base)]
        Task<CreateCommandResponse<int>> CreateEntry([Body] CreateCommandRequest<MusicianContactCreateModel> request);

        [Put(RoutesHelper.MusicianContact.Id)]
        Task<UpdateCommandResponse> UpdateEntry(int id, [Body] UpdateCommandRequest<MusicianContactUpdateModel> request);

        [Delete(RoutesHelper.MusicianContact.Id)]
        Task<DeleteCommandResponse> DeleteEntry(int id);
    }
}
