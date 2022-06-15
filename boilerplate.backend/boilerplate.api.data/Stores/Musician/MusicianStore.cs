using AutoMapper;
using boilerplate.api.common.Models;
using boilerplate.api.core.Models;
using boilerplate.api.data.Models;
using boilerplate.api.data.Procedures;
using System;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public class MusicianStore: BaseStore, IMusicianStore
    {
        public MusicianStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
            : base(connectionStrings, context, mapper)
        {   
        }

        protected override Type DataModelType => typeof(Musician);
        protected override string EntriesProcedureName => MusicianGetEntriesProcedure.Name;
        protected override string EntryProcedureName => MusicianGetEntriesProcedure.Name;

        public async Task<EntriesQueryResponse<AllMusiciansReportModel>> GetAllMusiciansAsync()
        {
            return await GetEntries<AllMusiciansReportModel>($@"SELECT {nameof(Musician.Id)} as {nameof(MusicianReportModel.Id)}, 
                                                            {nameof(Musician.Name)} as {nameof(MusicianReportModel.Name)} 
                                                            FROM {MusicianExtension.TABLE_NAME}");
        }
    }
}
