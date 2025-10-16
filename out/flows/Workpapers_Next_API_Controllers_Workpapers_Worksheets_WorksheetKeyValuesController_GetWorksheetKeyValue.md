[web] GET /api/binders/{binderId:Guid}/worksheets/{worksheetId:guid}/key-values/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.WorksheetKeyValuesController.GetWorksheetKeyValue)  [L39–L50] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to KeyValueDto [L44]
  └─ calls WorksheetRepository.ReadQuery [L44]
  └─ query Worksheet [L44]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessWorksheetQuery [L42]
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

