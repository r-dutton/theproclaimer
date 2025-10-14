[web] GET /api/accounting/ledger/accounts/source-accounts  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetLinkedSourceAccounts)  [L107–L121] [auth=user]
  └─ maps_to SourceAccountMappingDto [L115]
    └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
    └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountMappingDto) [L613]
  └─ calls SourceAccountRepository.ReadQuery [L115]
  └─ queries SourceAccount [L115]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L115]
  └─ sends_request CanIAccessFileQuery [L114]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L71]
        └─ method DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache [L68]
        └─ method CreateAccessKey [write] [L68]

