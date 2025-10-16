[web] POST /api/ui/documents/{documentId:guid}/versions/  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.AddDocumentVersion)  [L67–L73] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request AddDocumentVersionCommand [L72]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.AddDocumentVersionCommandHandler.Handle [L27–L94]
      └─ uses_service UserService
        └─ method GetUserId [L66]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service RequestInfoService
        └─ method IsM2MRequest [L67]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsM2MRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsM2MRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsM2MRequest [L11-L92]
      └─ uses_service IControlledRepository<Document>
        └─ method WriteQuery [L57]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentStatus>
        └─ method WriteQuery [L78]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method ReadQuery [L61]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L88]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
  └─ sends_request CanIAccessDocumentQuery [L71]
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

