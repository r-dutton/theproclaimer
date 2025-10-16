[web] GET /api/accounting/ledger/accounts/source-accounts  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetLinkedSourceAccounts)  [L107–L121] status=200 [auth=user]
  └─ maps_to SourceAccountMappingDto [L115]
    └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountMappingDto) [L274]
    └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountMappingDto) [L613]
  └─ calls SourceAccountRepository.ReadQuery [L115]
  └─ query SourceAccount [L115]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L115]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L114]
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

