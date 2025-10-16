[web] GET /api/internal/documents/{id:Guid}/data  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.ReadDocumentData)  [L322–L326] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentDataQuery [L325]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDataQueryHandler.Handle [L28–L63]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L54]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L43]
          └─ ... (no implementation details available)

