using boilerplate.api.common.Models;
using boilerplate.api.data.Extensions;
using boilerplate.api.domain.Services;
using boilerplate.api.domain.Services.Contact;
using boilerplate.api.domain.Services.MusicLabelContact;
using boilerplate.api.domain.Services.PlatformContact;
using Microsoft.Extensions.DependencyInjection;

namespace boilerplate.api.domain.Extensions
{
    public static class DependencyExtension
    {
        public static void RegisterDomainDependencies(this IServiceCollection services, ConnectionStrings connectionStrings)
        {
            services.RegisterDataDependencies(connectionStrings);

            services.AddTransient<IMusicianService, MusicianService>();
            services.AddTransient<IMusicLabelService, MusicLabelService>();
            services.AddTransient<IPlatformService, PlatformService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IMusicianContactService, MusicianContactService>();
            services.AddTransient<IPlatformContactService, PlatformContactService>();
            services.AddTransient<IMusicLabelContactService, MusicLabelContactService>();
        }
    }
}
