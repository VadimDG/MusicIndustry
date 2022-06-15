using AutoMapper;
using boilerplate.api.core.Models;
using boilerplate.api.data.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public class PlatformService : BaseService, IPlatformService
    {
        private readonly IPlatformStore _store;
        private readonly ILogger<PlatformService> _logger;

        public PlatformService(IPlatformStore store, ILogger<PlatformService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {
            _store = store;
            _logger = logger;
        }

        public async Task<EntriesQueryResponse<AllPlatformsReportModel>> GetAllPlatforms()
        {
            try
            {
                return await _store.GetAllPlatforms();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new EntriesQueryResponse<AllPlatformsReportModel>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
            
        }
    }
}
