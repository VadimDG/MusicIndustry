using AutoMapper;
using boilerplate.api.common.Models;
using boilerplate.api.core.Models;
using boilerplate.api.data.Models;
using boilerplate.api.data.Procedures;
using System;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public class PlatformStore : BaseStore, IPlatformStore
    {
        public PlatformStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
            : base(connectionStrings, context, mapper)
        {

        }

        protected override Type DataModelType => typeof(Platform);
        protected override string EntriesProcedureName => PlatformGetEntriesProcedure.Name;
        protected override string EntryProcedureName => PlatformGetEntriesProcedure.Name;

        public Task<EntriesQueryResponse<AllPlatformsReportModel>> GetAllPlatforms()
        {
            return GetEntries<AllPlatformsReportModel>(@$"SELECT {nameof(Platform.Id)} as {nameof(AllPlatformsReportModel.Id)}, 
                                                                 {nameof(Platform.Name)} as {nameof(AllPlatformsReportModel.Name)}
                                                            FROM {PlatformExtension.TABLE_NAME}");
        }
    }
}
