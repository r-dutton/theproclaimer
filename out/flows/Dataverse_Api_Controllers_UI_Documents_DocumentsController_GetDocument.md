[web] GET /api/ui/documents/documents/{id}  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetDocument)  [L138–L147] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentDto [L142]
    └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L404]
  └─ calls DocumentRepository.ReadQuery [L142]
  └─ queries Document [L142]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L142]
  └─ sends_request CanIAccessDocumentQuery [L141]
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

