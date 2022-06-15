using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Command.PlatformContact;
using boilerplate.api.core.Models.Query.PlatformContact;
using Refit;
using System.Threading.Tasks;

namespace boilerplate.api.core.Clients
{
    public interface IPlatformContactClient
    {
        [Get(RoutesHelper.PlatformContact.Id)]
        Task<EntriesQueryResponse<PlatformContactReportModel>> GetEntries(int id);

        [Post(RoutesHelper.PlatformContact.Base)]
        Task<CreateCommandResponse<int>> CreateEntry([Body] CreateCommandRequest<PlatformContactCreateModel> request);

        [Put(RoutesHelper.PlatformContact.Id)]
        Task<UpdateCommandResponse> UpdateEntry(int id, [Body] UpdateCommandRequest<PlatformContactUpdateModel> request);

        [Delete(RoutesHelper.PlatformContact.Id)]
        Task<DeleteCommandResponse> DeleteEntry(int id);
    }
}
