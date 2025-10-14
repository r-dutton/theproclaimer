[web] GET /api/internal/documents/find-documents  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.FindDocuments)  [L238–L294] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request FindDocumentsQuery [L268]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.FindDocumentsQueryHandler.Handle [L77–L428]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L104]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L103]
      └─ uses_service IControlledRepository<DocumentTag>
        └─ method ReadQuery [L373]
      └─ uses_service UserService
        └─ method GetUserAsync [L104]

