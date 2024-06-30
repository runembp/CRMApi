using System.Runtime.Serialization;
using Microsoft.OData.Client;

namespace CRMApi.Entities;

[Key(EntityLogicalName)]
public class AccountEntity
{
    [DataMember(Name = PrimaryKey)]
    public int AccountId { get; set; }
    
    [DataMember(Name = FieldExternalSupplierUpdated)]
    public DateTime ExternalSupplierUpdated { get; set; }
    
    public const string EntityLogicalName = "account";
    public const string EntityPluralName = "accounts";
    public const string PrimaryKey = "accountid";
    public const string UniqueIdentifier = "name";
    
    public const string FieldExternalSupplierUpdated = "new_externalsupplierupdated";
    public const string FieldRemarksAboutHealth = "new_remarksabouthealth";
    public const string FieldExternalSuppliers = "new_externalsuppliers";
    public const string FieldCoveragePerEmployeeGroup = "new_dkningprmedarbejdergruppe";
    public const string FieldBlumeSupport = "new_blumesupport";
    public const string FieldFfKey = "new_ffkeyn16";
}