[web] DELETE /api/accounting/tradingAccounts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.TradingAccountsController.Delete)  [L95–L102] status=200 [auth=user]
  └─ calls TradingAccountRepository.Remove [L100]
  └─ calls TradingAccountRepository.WriteQuery [L98]
  └─ delete TradingAccount [L100]
    └─ reads_from TradingAccounts
  └─ write TradingAccount [L98]
    └─ reads_from TradingAccounts
  └─ uses_service IControlledRepository<TradingAccount>
    └─ method WriteQuery [L98]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L99]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]

