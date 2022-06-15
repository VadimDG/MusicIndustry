using boilerplate.api.core.Clients;
using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Command.PlatformContact;
using boilerplate.api.core.Models.Query.PlatformContact;
using boilerplate.api.domain.Services.PlatformContact;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boilerplate.api.Controllers
{
    [ApiController]
    public class PlatformContactController : ControllerBase, IPlatformContactClient
    {
        private readonly IPlatformContactService _service;
        public PlatformContactController(IPlatformContactService service)
        {
            _service = service;
        }

        [HttpGet(RoutesHelper.PlatformContact.Id)]
        [ProducesResponseType(200, Type = typeof(EntriesQueryResponse<PlatformContactReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntriesQueryResponse<PlatformContactReportModel>> GetEntries([FromRoute] int id)
        {   
            return await _service.GetPlatformsByContactIdsAsync(new List<int> { id });
        }

        [HttpPost(RoutesHelper.PlatformContact.Base)]
        [ProducesResponseType(200, Type = typeof(CreateCommandResponse<int>))]
        [ProducesDefaultResponseType]
        public async Task<CreateCommandResponse<int>> CreateEntry([FromBody] CreateCommandRequest<PlatformContactCreateModel> request)
        {
            return await _service.CreateEntry<PlatformContactCreateModel, int>(request);
        }

        [HttpPut(RoutesHelper.PlatformContact.Id)]
        [ProducesResponseType(200, Type = typeof(UpdateCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<UpdateCommandResponse> UpdateEntry([FromRoute] int id, [FromBody] UpdateCommandRequest<PlatformContactUpdateModel> request)
        {
            return await _service.UpdateEntry<PlatformContactUpdateModel, int>(request);
        }

        [HttpDelete(RoutesHelper.PlatformContact.Id)]
        [ProducesResponseType(200, Type = typeof(DeleteCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<DeleteCommandResponse> DeleteEntry([FromRoute] int id)
        {
            return await _service.DeleteEntry(new DeleteCommandRequest<int> { Id = id });
        }        
    }
}
