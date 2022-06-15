using AutoMapper;
using boilerplate.api.common.Models;
using System;

namespace boilerplate.api.data.Stores.MusicianContact
{
    public class MusicianContactStore : BaseStore, IMusicianContactStore
    {
        public MusicianContactStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
            : base(connectionStrings, context, mapper)
        {

        }

        protected override Type DataModelType => typeof(Models.MusicianContact);
        protected override string EntriesProcedureName => null;
        protected override string EntryProcedureName => null;
    }
}
