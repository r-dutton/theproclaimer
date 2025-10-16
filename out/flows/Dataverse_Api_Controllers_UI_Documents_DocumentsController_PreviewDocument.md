[web] GET /api/ui/documents/documents/{id}/{fileExtension}/preview  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.PreviewDocument)  [L198–L205] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L201]
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
  └─ sends_request GetDocumentPreviewLink [L202]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentPreviewLinkHandler.Handle [L34–L119]
      └─ maps_to SecureDocumentStoreDto [L107]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorageDocumentWithService [L90]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L98]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L69]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L107]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L114]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service string[]
        └─ method Contains [L67]
          └─ ... (no implementation details available)

