[web] GET /api/ui/documents/documents/{id}/{versionId}/preview-by-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.PreviewDocumentByVersion)  [L207–L214] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L210]
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
  └─ sends_request GetDocumentPreviewLinkByVersionQuery [L211]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentPreviewLinkByVersionQueryHandler.Handle [L35–L135]
      └─ maps_to SecureDocumentStoreDto [L86]
      └─ maps_to DocumentVersionLightDto [L125]
        └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionLightDto) [L421]
      └─ uses_service DocumentServiceFactory
        └─ method GetForStore [L86]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L77]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L64]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L86]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L131]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service string[]
        └─ method Contains [L72]
          └─ ... (no implementation details available)

