[web] GET /api/ui/documents/documents/{id}/meta-data  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetMetaData)  [L169–L187] [auth=Authentication.UserPolicy]
  └─ calls DocumentVersionRepository.ReadQuery [L174]
  └─ queries DocumentVersion [L174]
    └─ reads_from DocumentVersions
  └─ uses_service IControlledRepository<DocumentVersion>
    └─ method ReadQuery [L174]
  └─ sends_request CanIAccessDocumentQuery [L172]
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

