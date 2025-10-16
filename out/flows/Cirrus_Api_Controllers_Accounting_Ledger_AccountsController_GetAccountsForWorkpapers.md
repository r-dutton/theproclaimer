[web] GET /api/accounting/ledger/accounts/{fileId:Guid}/workpapers-info  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetAccountsForWorkpapers)  [L189–L195] status=200 [auth=user]
  └─ sends_request GetAccountsForWorkpapersQuery [L193]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAccountsForWorkpapersQueryHandler.Handle [L25–L74]
      └─ maps_to AccountInfoForWorkpapersDto [L40]
        └─ automapper.registration CirrusMappingProfile (Account->AccountInfoForWorkpapersDto) [L354]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L40]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L59]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L42]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L192]
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

