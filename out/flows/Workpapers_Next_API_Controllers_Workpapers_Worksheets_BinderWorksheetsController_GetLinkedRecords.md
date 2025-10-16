[web] GET /api/binders/{binderId:Guid}/worksheets/{id:Guid}/linked-records  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetLinkedRecords)  [L126–L138] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to LinkedWorkpaperRecordDto [L131]
    └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->LinkedWorkpaperRecordDto) [L559]
  └─ calls WorkpaperRecordRepository.ReadQuery [L131]
  └─ query WorkpaperRecord [L131]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L131]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessWorksheetQuery [L129]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Worksheets.CanIAccessWorksheetQueryHandler.Handle [L36–L60]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L54]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

