[web] POST /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.CreateWorksheetKeyValue)  [L68–L79] status=201 [auth=AuthorizationPolicies.User]
  └─ calls WorksheetRepository.WriteQuery [L71]
  └─ write Worksheet [L71]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method WriteQuery [L71]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessWorksheetQuery [L74]
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

