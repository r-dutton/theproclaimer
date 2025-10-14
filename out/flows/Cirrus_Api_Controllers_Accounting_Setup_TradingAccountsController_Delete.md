[web] DELETE /api/accounting/tradingAccounts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.TradingAccountsController.Delete)  [L95–L102] [auth=user]
  └─ calls TradingAccountRepository.Remove [L100]
  └─ calls TradingAccountRepository.WriteQuery [L98]
  └─ writes_to TradingAccount [L100]
    └─ reads_from TradingAccounts
  └─ writes_to TradingAccount [L98]
    └─ reads_from TradingAccounts
  └─ uses_service IControlledRepository<TradingAccount>
    └─ method WriteQuery [L98]
  └─ sends_request CanIAccessFileQuery [L99]
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

