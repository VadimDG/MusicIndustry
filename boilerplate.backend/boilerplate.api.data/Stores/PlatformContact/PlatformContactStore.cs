using AutoMapper;
using boilerplate.api.common.Models;
using System;

namespace boilerplate.api.data.Stores.PlatformContact
{
    public class PlatformContactStore : BaseStore, IPlatformContactStore
    {
        public PlatformContactStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
            : base(connectionStrings, context, mapper)
        {

        }

        protected override Type DataModelType => typeof(Models.PlatformContact);
        protected override string EntriesProcedureName => null;
        protected override string EntryProcedureName => null;
    }
}
