using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores.Contact
{
    public interface IContactStore : IBaseStore
    {
        Task<EntryQueryResponse<Models.Contact>> GetContactById(int id);
        Task<bool> UpdateContact(UpdateCommandRequest<Models.Contact> request);
    }
}
