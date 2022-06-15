using boilerplate.api.core.Models;
using boilerplate.api.data.Models;

namespace boilerplate.api.data.Procedures
{
    [Procedure]
    public static class ContactGetEntriesProcedure
    {
        public static string Name => "procContactGetEntries";
        public static int Version => 4;
        public static string Text => $@"
/* version={Version} */
CREATE PROCEDURE [{Name}]
    {ProcedureParams.AllowDirtyRead} BIT,
    {ProcedureParams.Offset} INT = 0,
    {ProcedureParams.Limit} INT = 1,
    {ProcedureParams.Id} INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF {ProcedureParams.AllowDirtyRead} = 1
    BEGIN
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    END

    SELECT
    m.[{nameof(Contact.Id)}] AS [{nameof(ContactDisplayReportModel.Id)}],
    m.[{nameof(Contact.FirstName)}] AS [{nameof(ContactDisplayReportModel.FirstName)}],
    m.[{nameof(Contact.LastName)}] AS [{nameof(ContactDisplayReportModel.LastName)}],    
    m.[{nameof(Contact.Email)}] AS [{nameof(ContactDisplayReportModel.Email)}],
    m.[{nameof(Contact.PhoneCell)}] AS [{nameof(ContactDisplayReportModel.PhoneCell)}],
    m.[{nameof(Contact.PhoneBusiness)}] AS [{nameof(ContactDisplayReportModel.PhoneBusiness)}],
    m.[{nameof(Contact.IsActive)}] AS [{nameof(ContactDisplayReportModel.Active)}],
    (SELECT COUNT(*) FROM [{MusicianContactExtension.TABLE_NAME}] mc WHERE mc.[{nameof(MusicianContact.ContactId)}] = m.[{nameof(Contact.Id)}]) as [{nameof(ContactDisplayReportModel.Musicians)}],
	(SELECT COUNT(*) FROM [{MusicLabelContactExtension.TABLE_NAME}] mlc WHERE mlc.[{nameof(MusicLabelContact.ContactId)}] = m.[{nameof(Contact.Id)}]) as [{nameof(ContactDisplayReportModel.Labels)}],
	(SELECT COUNT(*) FROM [{PlatformContactExtension.TABLE_NAME}] pc WHERE pc.[{nameof(PlatformContact.ContactId)}] = m.[{nameof(Contact.Id)}]) as [{nameof(ContactDisplayReportModel.Platforms)}]
    FROM [{ContactExtension.TABLE_NAME}] m
    WHERE {ProcedureParams.Id} IS NULL OR m.[{nameof(Contact.Id)}] = {ProcedureParams.Id}    
    ORDER BY m.[{nameof(Contact.Id)}]
    OFFSET {ProcedureParams.Offset} ROWS FETCH NEXT {ProcedureParams.Limit} ROWS ONLY
    
    IF {ProcedureParams.Id} IS NULL
    BEGIN
        SELECT COUNT(*)
        FROM [{ContactExtension.TABLE_NAME}] m
    END
END";
    }
}
