[web] GET /api/binders/{binderId:Guid}/worksheets/{id:guid}/hyperlinks  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetHyperlinks)  [L285–L305] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to HyperlinkDto [L296]
  └─ maps_to HyperlinkDto [L290]
  └─ calls WorkpaperRecordRepository.ReadQuery [L296]
  └─ calls WorksheetRepository.ReadQuery [L290]
  └─ query WorkpaperRecord [L296]
    └─ reads_from WorkpaperRecords
  └─ query Worksheet [L290]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L296]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L290]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessWorksheetQuery [L288]
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

