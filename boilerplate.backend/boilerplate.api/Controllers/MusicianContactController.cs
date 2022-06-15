using boilerplate.api.core.Clients;
using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Command.MusicianContact;
using boilerplate.api.core.Models.Query.MusicianContact;
using boilerplate.api.domain.Services.PlatformContact;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boilerplate.api.Controllers
{
    [ApiController]
    public class MusicianContactController : ControllerBase, IMusicianContactClient
    {
        private readonly IMusicianContactService _service;
        public MusicianContactController(IMusicianContactService service)
        {
            _service = service;
        }

        [HttpGet(RoutesHelper.MusicianContact.Id)]
        [ProducesResponseType(200, Type = typeof(EntriesQueryResponse<MusicianContactReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntriesQueryResponse<MusicianContactReportModel>> GetEntries([FromRoute] int id)
        {
            return await _service.GetMusiciansByContactIdsAsync(new List<int> { id });
        }

        [HttpPost(RoutesHelper.MusicianContact.Base)]
        [ProducesResponseType(200, Type = typeof(CreateCommandResponse<int>))]
        [ProducesDefaultResponseType]
        public async Task<CreateCommandResponse<int>> CreateEntry([FromBody] CreateCommandRequest<MusicianContactCreateModel> request)
        {
            return await _service.CreateEntry<MusicianContactCreateModel, int>(request);
        }

        [HttpPut(RoutesHelper.MusicianContact.Id)]
        [ProducesResponseType(200, Type = typeof(UpdateCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<UpdateCommandResponse> UpdateEntry([FromRoute] int id, [FromBody] UpdateCommandRequest<MusicianContactUpdateModel> request)
        {
            return await _service.UpdateEntry<MusicianContactUpdateModel, int>(request);
        }

        [HttpDelete(RoutesHelper.MusicianContact.Id)]
        [ProducesResponseType(200, Type = typeof(DeleteCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<DeleteCommandResponse> DeleteEntry([FromRoute] int id)
        {
            return await _service.DeleteEntry(new DeleteCommandRequest<int> { Id = id });
        }        
    }
}
