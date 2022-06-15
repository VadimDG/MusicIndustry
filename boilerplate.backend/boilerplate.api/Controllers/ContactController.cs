using boilerplate.api.core.Clients;
using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.domain.Services.Contact;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace boilerplate.api.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase, IContactClient
    {
        private readonly IContactService _service;
        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet(RoutesHelper.Contact.Base)]
        [ProducesResponseType(200, Type = typeof(EntriesQueryResponse<ContactDisplayReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntriesQueryResponse<ContactDisplayReportModel>> GetEntries([FromQuery] EntriesQueryRequest request)
        {
            return await _service.GetEntries<ContactDisplayReportModel>(request);
        }

        [HttpPost(RoutesHelper.Contact.Base)]
        [ProducesResponseType(200, Type = typeof(CreateCommandResponse<int>))]
        [ProducesDefaultResponseType]
        public async Task<CreateCommandResponse<int>> CreateEntry([FromBody] CreateCommandRequest<ContactCreateModel> request)
        {
            return await _service.CreateContact(request);
        }

        [HttpGet(RoutesHelper.Contact.Id)]
        [ProducesResponseType(200, Type = typeof(EntryQueryResponse<ContactEditReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntryQueryResponse<ContactEditReportModel>> GetEntry([FromRoute] int id)
        {
            return await _service.GetContactWithDependenciesAsync(new EntryQueryRequest<int> { Id = id });
        }

        [HttpPut(RoutesHelper.Contact.Id)]
        [ProducesResponseType(200, Type = typeof(UpdateCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<UpdateCommandResponse> UpdateEntry([FromRoute] int id, [FromBody] UpdateCommandRequest<ContactUpdateModel> request)
        {
            return await _service.UpdateContact(request);
        }

        [HttpDelete(RoutesHelper.Contact.Id)]
        [ProducesResponseType(200, Type = typeof(DeleteCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<DeleteCommandResponse> DeleteEntry([FromRoute] int id)
        {
            return await _service.DeleteEntry(new DeleteCommandRequest<int> { Id = id });
        }
    }
}
