[web] GET /api/ui/documents/documents/{id}/edit  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.EditDocument)  [L292–L299] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request EditDocumentCommand [L296]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.EditDocumentCommandHandler.Handle [L41–L125]
      └─ uses_service UserService
        └─ method GetUserId [L111]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L83]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentAuditLog>
        └─ method Add [L106]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L71]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L82]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ sends_request CanIAccessDocumentQuery [L295]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)

