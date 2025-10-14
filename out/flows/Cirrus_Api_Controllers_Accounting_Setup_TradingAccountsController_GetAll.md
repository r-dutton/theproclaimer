[web] GET /api/accounting/tradingAccounts/forfile/{fileId}  (Cirrus.Api.Controllers.Accounting.Setup.TradingAccountsController.GetAll)  [L56–L65] [auth=user]
  └─ maps_to TradingAccountDto [L60]
    └─ automapper.registration CirrusMappingProfile (TradingAccount->TradingAccountDto) [L227]
  └─ calls TradingAccountRepository.ReadQuery [L60]
  └─ queries TradingAccount [L60]
    └─ reads_from TradingAccounts
  └─ uses_service IControlledRepository<TradingAccount>
    └─ method ReadQuery [L60]
  └─ sends_request CanIAccessFileQuery [L59]
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

