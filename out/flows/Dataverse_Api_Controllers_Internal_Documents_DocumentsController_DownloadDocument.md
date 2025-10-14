[web] GET /api/internal/documents/{id:Guid}/download  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.DownloadDocument)  [L302–L310] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CanIAccessDocumentQuery [L306]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
  └─ sends_request GetDocumentDownloadLink [L307]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDownloadLinkHandler.Handle [L30–L77]
      └─ maps_to SecureDocumentStoreDto [L65]
      └─ uses_service DocumentServiceFactory
        └─ method GetForStore [L65]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L56]
      └─ uses_service IControlledRepository<DocumentAuditLog>
        └─ method Add [L68]
      └─ uses_service IMapper
        └─ method Map [L65]
      └─ uses_service UserService
        └─ method GetUserId [L73]

