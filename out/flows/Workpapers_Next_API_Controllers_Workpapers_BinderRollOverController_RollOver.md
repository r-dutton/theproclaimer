[web] PUT /api/binder-roll-over/  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.RollOver)  [L76–L89] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L79]
  └─ write Binder [L79]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L79]
      └─ ... (no implementation details available)
  └─ sends_request RollOverBinderCommand [L86]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.RollOverBinderCommandHandler.Handle [L57–L911]
      └─ maps_to BinderSectionCreateModel [L251]
      └─ maps_to RollOverHyperlinkModel [L353]
      └─ maps_to RollOverMatterModel [L349]
      └─ maps_to RollOverRecordModel [L368]
      └─ maps_to RollOverRecordModel [L345]
      └─ uses_service UserService
        └─ method GetUserId [L179]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L146]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
      └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping>
        └─ method ReadQuery [L320]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L251]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Matter>
        └─ method ReadQuery [L157]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<MatterStatus> (AddScoped)
        └─ method ReadQuery [L165]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<RollOverHyperlink>
        └─ method WriteQuery [L237]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<RollOverMatter>
        └─ method WriteQuery [L233]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<RollOverRecord>
        └─ method WriteQuery [L224]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<RollOverWorksheet>
        └─ method WriteQuery [L229]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L298]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L499]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L254]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L112]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service UnitOfWork
        └─ method Table [L387]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery [L85]
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

