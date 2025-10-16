[web] GET /api/internal/documents/{id:Guid}/download  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.DownloadDocument)  [L302–L310] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CanIAccessDocumentQuery [L306]
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
  └─ sends_request GetDocumentDownloadLink [L307]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDownloadLinkHandler.Handle [L30–L77]
      └─ maps_to SecureDocumentStoreDto [L65]
      └─ uses_service UserService
        └─ method GetUserId [L73]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service DocumentServiceFactory
        └─ method GetForStore [L65]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L56]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentAuditLog>
        └─ method Add [L68]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L65]
          └─ ... (no implementation details available)

