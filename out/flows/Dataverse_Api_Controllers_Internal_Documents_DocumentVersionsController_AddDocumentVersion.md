[web] POST /api/internal/documents/{documentId:guid}/versions/  (Dataverse.Api.Controllers.Internal.Documents.DocumentVersionsController.AddDocumentVersion)  [L29–L34] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request AddDocumentVersionCommand [L33]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.AddDocumentVersionCommandHandler.Handle [L27–L94]
      └─ uses_service IControlledRepository<Document>
        └─ method WriteQuery [L57]
      └─ uses_service IControlledRepository<DocumentStatus>
        └─ method WriteQuery [L78]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method ReadQuery [L61]
      └─ uses_service RequestInfoService
        └─ method IsM2MRequest [L67]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L88]
      └─ uses_service UserService
        └─ method GetUserId [L66]

