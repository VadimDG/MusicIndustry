using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services.Contact
{
    public interface IContactService : IBaseService
    {
        Task<EntryQueryResponse<ContactEditReportModel>> GetContactWithDependenciesAsync(EntryQueryRequest<int> request);
        Task<CreateCommandResponse<int>> CreateContact(CreateCommandRequest<ContactCreateModel> request);
        Task<UpdateCommandResponse> UpdateContact(UpdateCommandRequest<ContactUpdateModel> request);
    }
}
