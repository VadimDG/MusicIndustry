using boilerplate.ui.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boilerplate.ui.Services.Contact
{
    public interface IContactService
    {
        Task<ServiceResult<ContactGetEntriesViewModel>> GetEntries(int offset, int limit);
        Task<ServiceResult<ContactCreateEntryViewModel>> GetCreateEntryViewModel();
        Task<ServiceResult> CreateEntry(ContactCreateEntryViewModel.FormModel model);
        Task<ServiceResult<ContactUpdateEntryViewModel>> GetUpdateEntryViewModel(int id);
        Task<ServiceResult> UpdateEntry(ContactUpdateEntryViewModel.FormModel model);
        Task<ServiceResult> DeleteEntry(int id);
        List<Relationship> GetContactRelationshipsList(Dictionary<string, List<Relationship>> relationshipDictionary);
        Task<Dictionary<string, List<Relationship>>> GetContactRelationshipsDictionaryAsync();

    }
}
