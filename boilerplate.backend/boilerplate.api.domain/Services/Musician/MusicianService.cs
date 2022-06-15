using AutoMapper;
using boilerplate.api.core.Models;
using boilerplate.api.data.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public class MusicianService: BaseService, IMusicianService
    {
        private readonly IMusicianStore _store;
        private readonly ILogger<MusicianService> _logger;

        public MusicianService(IMusicianStore store, ILogger<MusicianService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {   
            _store = store;
            _logger = logger;
        }

        public async Task<EntriesQueryResponse<AllMusiciansReportModel>> GetAllMusicians()
        {
            try
            {
                return await _store.GetAllMusiciansAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new EntriesQueryResponse<AllMusiciansReportModel>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}