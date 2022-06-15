using AutoMapper;
using boilerplate.api.core.Models;
using boilerplate.api.data.Stores.Contact;
using boilerplate.api.domain.Services.MusicLabelContact;
using boilerplate.api.domain.Services.PlatformContact;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services.Contact
{
    public class ContactService : BaseService, IContactService
    {
        private readonly IMusicianContactService _musicianContactService;
        private readonly IMusicLabelContactService _musicLabelContactService;
        private readonly IPlatformContactService _platformContactService;
        private readonly IContactStore _store;
        private readonly ILogger _logger;

        public ContactService(IContactStore store, IMusicianContactService musicianContactService,
            IMusicLabelContactService musicLabelContactService, IPlatformContactService platformContactService,
            ILogger<ContactService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {
            _store = store;
            _logger = logger;
            _musicianContactService = musicianContactService;
            _musicLabelContactService = musicLabelContactService;
            _platformContactService = platformContactService;
        }

        public async Task<EntryQueryResponse<ContactEditReportModel>> GetContactWithDependenciesAsync(EntryQueryRequest<int> request)
        {
            try
            {
                var contact = await _store.GetContactById(request.Id);

                if (!contact.Success)
                {
                    return new EntryQueryResponse<ContactEditReportModel>
                    {
                        Code = contact.Code,
                        ErrorMessage = contact.ErrorMessage,
                        Success = contact.Success
                    };
                }

                var relatedMusicianIds = (await _musicianContactService.GetMusiciansByContactIdsAsync(new List<int> { contact.Data.Id })).Data.Select(x => x.MusicianId).ToList();
                var relatedMusicLabelIds = (await _musicLabelContactService.GetMusicLabelsByContactIdsAsync(new List<int> { contact.Data.Id })).Data.Select(x => x.MusicLabelId).ToList();
                var relatedPlatformIds = (await _platformContactService.GetPlatformsByContactIdsAsync(new List<int> { contact.Data.Id })).Data.Select(x => x.PlatformId).ToList();

                var response = new EntryQueryResponse<ContactEditReportModel>
                {
                    Code = contact.Code,
                    ErrorMessage = contact.ErrorMessage,
                    Success = contact.Success,
                    Data = _mapper.Map<ContactEditReportModel>(contact.Data)
                };

                response.Data.Musicians = relatedMusicianIds;
                response.Data.MusicLabels = relatedMusicLabelIds;
                response.Data.Platforms = relatedPlatformIds;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new EntryQueryResponse<ContactEditReportModel>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }

        }

        public async Task<CreateCommandResponse<int>> CreateContact(CreateCommandRequest<ContactCreateModel> request)
        {
            try
            {
                return await CreateEntry<data.Models.Contact, int>(new CreateCommandRequest<data.Models.Contact>
                {
                    Entry = _mapper.Map<data.Models.Contact>(request.Entry)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CreateCommandResponse<int>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<UpdateCommandResponse> UpdateContact(UpdateCommandRequest<ContactUpdateModel> request)
        {
            try
            {
                var Entry = _mapper.Map<data.Models.Contact>(request.Entry);

                var success = await _store.UpdateContact(new UpdateCommandRequest<data.Models.Contact>
                {
                    Entry = Entry
                });
                if (!success)
                {
                    return new UpdateCommandResponse
                    {
                        Success = false,
                        Code = ResponseCode.NotFound
                    };
                }
                return new UpdateCommandResponse
                {
                    Success = true,
                    Code = ResponseCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new UpdateCommandResponse
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
