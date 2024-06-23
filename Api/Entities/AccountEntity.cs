namespace CRMApi.Entities;

public static class AccountEntity
{
    public const string EntityLogicalName = "account";
    public const string EntityPluralName = "accounts";
    public const string PrimaryKey = "accountid";
    public const string UniqueIdentifier = "name";
    
    public const string ExternalSupplierUpdated = "new_externalsupplierupdated";
    public const string RemarksAboutHealth = "new_remarksabouthealth";
    public const string ExternalSuppliers = "new_external_suppliers";
    public const string CoveragePerEmployeeGroup = "new_dkningprmedarbejdergruppe";
    public const string BlumeSupport = "new_blumesupport";
    public const string FfKey = "new_ffkey";
}