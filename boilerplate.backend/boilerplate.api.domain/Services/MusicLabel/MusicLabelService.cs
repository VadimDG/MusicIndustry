using AutoMapper;
using boilerplate.api.core.Models;
using boilerplate.api.data.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public class MusicLabelService : BaseService, IMusicLabelService
    {
        private readonly IMusicLabelStore _store;
        private readonly ILogger<MusicLabelService> _logger;

        public MusicLabelService(IMusicLabelStore store, ILogger<MusicLabelService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {
            _store = store;
            _logger = logger;
        }

        public async Task<EntriesQueryResponse<AllMusicLabelsReportModel>> GetAllMusicLabels()
        {
            try
            {
                return await _store.GetAllMusicLabels();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new EntriesQueryResponse<AllMusicLabelsReportModel>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
