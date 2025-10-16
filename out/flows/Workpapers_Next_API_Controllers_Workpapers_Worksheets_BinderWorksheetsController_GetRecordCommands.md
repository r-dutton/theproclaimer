[web] GET /api/binders/{binderId:Guid}/worksheets/{id:Guid}/commands  (Workpapers.Next.API.Controllers.Workpapers.Worksheets.BinderWorksheetsController.GetRecordCommands)  [L140–L154] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to CommandDto [L145]
  └─ calls WorksheetRepository.ReadQuery [L145]
  └─ query Worksheet [L145]
    └─ reads_from Worksheets
  └─ uses_service IControlledRepository<Worksheet>
    └─ method ReadQuery [L145]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessWorksheetQuery [L143]
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

