[web] GET /api/internal/documents/{id:Guid}/data  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.ReadDocumentData)  [L322–L326] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentDataQuery [L325]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDataQueryHandler.Handle [L28–L62]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L54]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L43]

