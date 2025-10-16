[web] PUT /api/workpaper-records/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.Update)  [L199–L225] status=200 [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L202]
  └─ write WorkpaperRecord [L202]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method WriteQuery [L202]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessWorkpaperRecordQuery [L210]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.CanIAccessWorkpaperRecordQueryHandler.Handle [L35–L59]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L53]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L55]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L57]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ sends_request GetWorkpaperRecordParamsQuery [L221]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.GetWorkpaperRecordParamsQueryHandler.Handle [L76–L264]
      └─ uses_service UserService
        └─ method GetUserId [L115]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L153]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
        └─ method WriteQuery [L169]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L251]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L204]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Worksheet>
        └─ method WriteQuery [L141]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L189]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

