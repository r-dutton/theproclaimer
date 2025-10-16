[web] GET /api/accounting/ledger/source-accounts/for-source/{sourceId}  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForSource)  [L111–L133] status=200 [auth=user]
  └─ calls SourceRepository.ReadQuery [L119]
  └─ query Source [L119]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L119]
      └─ ... (no implementation details available)
  └─ sends_request GetSourceAccountsWithCachedQuery [L127]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetSourceAccountsWithCachedQueryHandler.Handle [L34–L96]
      └─ maps_to SourceAccountWithCachedDto [L53]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountWithCachedDto) [L289]
      └─ maps_to SourceAccountWithCachedDto [L84]
      └─ uses_service IControlledRepository<CachedSourceAccount>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L53]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L58]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L123]
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

