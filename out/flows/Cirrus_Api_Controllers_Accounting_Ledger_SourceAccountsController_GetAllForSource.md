[web] GET /api/accounting/ledger/source-accounts/for-source/{sourceId}  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForSource)  [L111–L133] [auth=user]
  └─ calls SourceRepository.ReadQuery [L119]
  └─ queries Source [L119]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L119]
  └─ sends_request GetSourceAccountsWithCachedQuery [L127]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetSourceAccountsWithCachedQueryHandler.Handle [L34–L96]
      └─ maps_to SourceAccountWithCachedDto [L53]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountWithCachedDto) [L289]
      └─ maps_to SourceAccountWithCachedDto [L84]
      └─ uses_service IControlledRepository<CachedSourceAccount>
        └─ method ReadQuery [L68]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L53]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L58]
  └─ sends_request CanIAccessFileQuery [L123]
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

