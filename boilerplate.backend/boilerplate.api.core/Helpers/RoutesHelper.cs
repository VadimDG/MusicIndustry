namespace boilerplate.api.core.Helpers
{
    public static class RoutesHelper
    {
        public static class Musician
        {
            public const string Base = "/api/musicians";
            public const string BaseAll = "/api/musiciansall";
            public const string Id = Base + "/{id}";
        }

        public static class MusicLabel
        {
            public const string Base = "/api/musicLabels";
            public const string BaseAll = "/api/musicLabelsall";
            public const string Id = Base + "/{id}";
        }

        public static class Platform
        {
            public const string Base = "/api/platforms";
            public const string BaseAll = "/api/platformsall";
            public const string Id = Base + "/{id}";
        }

        public static class Contact
        {
            public const string Base = "/api/contacts";
            public const string Id = Base + "/{id}";
        }

        public static class PlatformContact
        {
            public const string Base = "/api/platformcontacts";
            public const string Id = Base + "/{id}";
        }

        public static class MusicianContact
        {
            public const string Base = "/api/musiciancontacts";
            public const string Id = Base + "/{id}";
        }

        public static class MusicLabelContact
        {
            public const string Base = "/api/musiclabelcontacts";
            public const string Id = Base + "/{id}";
        }
    }
}
