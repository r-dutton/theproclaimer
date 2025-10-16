[web] GET /api/import-runs/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.Get)  [L51–L64] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to JournalLightDto [L60]
  └─ maps_to ImportRunDto [L54]
    └─ automapper.registration CirrusMappingProfile (ImportRun->ImportRunDto) [L517]
    └─ automapper.registration WorkpapersMappingProfile (ImportRun->ImportRunDto) [L817]
  └─ calls ImportRunRepository.ReadQuery [L54]
  └─ query ImportRun [L54]
    └─ reads_from ImportRuns
  └─ uses_service IControlledRepository<ImportRun>
    └─ method ReadQuery [L54]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L60]
      └─ ... (no implementation details available)
  └─ sends_request GetImportRunJournalsQuery [L59]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetImportRunJournalsQueryHandler.Handle [L28–L83]
      └─ maps_to JournalCreateModel [L59]
        └─ converts_to JournalLightDto [L491]
        └─ converts_to JournalLightDto [L861]
  └─ sends_request CanIAccessBinderQuery [L57]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]

