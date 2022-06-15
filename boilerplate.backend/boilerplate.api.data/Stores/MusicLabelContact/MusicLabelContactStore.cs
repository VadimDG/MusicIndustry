using AutoMapper;
using boilerplate.api.common.Models;
using System;

namespace boilerplate.api.data.Stores.LabelContact
{
    public class MusicLabelContactStore : BaseStore, IMusicLabelContactStore
    {
        public MusicLabelContactStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
            : base(connectionStrings, context, mapper)
        {

        }

        protected override Type DataModelType => typeof(Models.MusicLabelContact);
        protected override string EntriesProcedureName => null;
        protected override string EntryProcedureName => null;
    }
}
