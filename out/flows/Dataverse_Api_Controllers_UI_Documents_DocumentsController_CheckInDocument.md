[web] POST /api/ui/documents/documents/{id}/check-in  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CheckInDocument)  [L234–L244] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request CheckInDocumentCommand [L241]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.CheckInDocumentCommandHandler.Handle [L35–L109]
      └─ uses_service UserService
        └─ method GetUserId [L83]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service UploadStorageService
        └─ method CopyDocument [L81]
          └─ implementation Dataverse.Services.DocumentStorage.UploadStorageService.CopyDocument [L17-L87]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L93]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Contact>
        └─ method ReadQuery [L101]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L67]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_storage UploadStorageService.CopyDocument [L81]
  └─ sends_request CanIAccessDocumentQuery [L240]
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

