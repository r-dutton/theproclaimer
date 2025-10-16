[web] POST /api/workpaper-records/{id:guid}/create-or-update-financial-report  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.CreateOrUpdateFinancialReport)  [L450–L475] status=201 [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L453]
  └─ write WorkpaperRecord [L453]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method WriteQuery [L453]
      └─ ... (no implementation details available)
  └─ sends_request CreateOrUpdateFinancialReportCommand [L471]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.CreateOrUpdateFinancialReportCommandHandler.Handle [L42–L215]
      └─ uses_service UserService
        └─ method GetUser [L118]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L156]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetStandardQueryParams [L17-L127]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetReportData [L82]
          └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetReportData [L18-L250]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L106]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L94]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ sends_request CreateOrUpdateFinancialReportCommand [L466]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.CreateOrUpdateFinancialReportCommandHandler.Handle [L42–L215]
      └─ uses_service UserService
        └─ method GetUser [L118]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L156]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetStandardQueryParams [L17-L127]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetReportData [L82]
          └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetReportData [L18-L250]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L106]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L94]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ sends_request CanIAccessBinderQuery [L457]
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

