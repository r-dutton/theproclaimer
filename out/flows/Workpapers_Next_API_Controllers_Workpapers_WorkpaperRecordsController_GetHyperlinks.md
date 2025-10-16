[web] GET /api/workpaper-records/{id:guid}/hyperlinks  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetHyperlinks)  [L399–L423] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to HyperlinkDto [L411]
  └─ maps_to HyperlinkDto [L404]
  └─ calls WorkpaperRecordRepository.ReadQuery [L404]
  └─ query WorkpaperRecord [L404]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L404]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessWorkpaperRecordQuery [L402]
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

