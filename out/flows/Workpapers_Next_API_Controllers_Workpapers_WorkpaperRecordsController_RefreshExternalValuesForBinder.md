[web] PUT /api/workpaper-records/external-values/refresh/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.RefreshExternalValuesForBinder)  [L227–L242] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L230]
  └─ query Binder [L230]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L230]
      └─ ... (no implementation details available)
  └─ sends_request RefreshExternalValuesCommand [L239]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.RefreshExternalValuesCommandHandler.Handle [L43–L282]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetDataset [L225]
          └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetDataset [L18-L250]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L125]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method WriteQuery [L70]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Worksheet>
        └─ method ReadQuery [L116]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L68]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ sends_request CanIAccessBinderQuery [L238]
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

