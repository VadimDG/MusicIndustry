using boilerplate.api.core.Clients;
using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.core.Models.Command.MusicLabelContact;
using boilerplate.api.core.Models.Query.MusicLabelContact;
using boilerplate.api.domain.Services.MusicLabelContact;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace boilerplate.api.Controllers
{
    [ApiController]
    public class MusicLabelContactController : ControllerBase, IMusicLabelContactClient
    {
        private readonly IMusicLabelContactService _service;
        public MusicLabelContactController(IMusicLabelContactService service)
        {
            _service = service;
        }

        [HttpGet(RoutesHelper.MusicLabelContact.Id)]
        [ProducesResponseType(200, Type = typeof(EntriesQueryResponse<MusicLabelContactReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntriesQueryResponse<MusicLabelContactReportModel>> GetEntries([FromRoute] int id)
        {
            return await _service.GetMusicLabelsByContactIdsAsync(new List<int> { id });
        }

        [HttpPost(RoutesHelper.MusicLabelContact.Base)]
        [ProducesResponseType(200, Type = typeof(CreateCommandResponse<int>))]
        [ProducesDefaultResponseType]
        public async Task<CreateCommandResponse<int>> CreateEntry([FromBody] CreateCommandRequest<MusicLabelContactCreateModel> request)
        {
            return await _service.CreateEntry<MusicLabelContactCreateModel, int>(request);
        }

        [HttpPut(RoutesHelper.MusicLabelContact.Id)]
        [ProducesResponseType(200, Type = typeof(UpdateCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<UpdateCommandResponse> UpdateEntry([FromRoute] int id, [FromBody] UpdateCommandRequest<MusicLabelContactUpdateModel> request)
        {
            return await _service.UpdateEntry<MusicLabelContactUpdateModel, int>(request);
        }

        [HttpDelete(RoutesHelper.MusicLabelContact.Id)]
        [ProducesResponseType(200, Type = typeof(DeleteCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<DeleteCommandResponse> DeleteEntry([FromRoute] int id)
        {
            return await _service.DeleteEntry(new DeleteCommandRequest<int> { Id = id });
        }
    }
}
