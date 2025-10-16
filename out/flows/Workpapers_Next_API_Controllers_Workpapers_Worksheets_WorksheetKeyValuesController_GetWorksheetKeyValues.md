[web] GET /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.GetWorksheetKeyValues)  [L52–L66] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to KeyValueDto [L57]
  └─ calls WorksheetRepository.ReadQuery [L57]
  └─ query Worksheet [L57]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L57]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessWorksheetQuery [L55]
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

