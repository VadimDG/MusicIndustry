namespace boilerplate.api.data.Procedures
{
    public static class ProcedureParams
    {
        public static string AllowDirtyRead => "@allowDirtyRead";
        public static string Offset => "@offset";
        public static string Limit => "@limit";
        public static string Id => "@id";
    }

    public static class ContactInsertProcedureParams
    {
        public static string FirstName => "@firstName";
        public static string LastName => "@lastName";
        public static string Title => "@title";
        public static string Company => "@company";
        public static string Email => "@email";
        public static string PhoneCell => "@phoneCell";
        public static string PhoneBusiness => "@phoneBusiness";
        public static string Fax => "@fax";
        public static string AddressLine1 => "@addressLine1";
        public static string AddressLine2 => "@addressLine2";
        public static string City => "@city";
        public static string State => "@state";
        public static string Zip => "@zip";
        public static string IsActive => "@isActive";
    }
}
