[web] PUT /api/workpaper-records/{id:Guid}/remove-linking  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.RemoveLinkedFlag)  [L300–L312] status=200 [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L303]
  └─ write WorkpaperRecord [L303]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method WriteQuery [L303]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessWorkpaperRecordQuery [L307]
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

