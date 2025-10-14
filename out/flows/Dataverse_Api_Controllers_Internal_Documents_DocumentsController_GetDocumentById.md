[web] GET /api/internal/documents/{id:Guid}  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.GetDocumentById)  [L296–L300] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentByIdQuery [L299]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentByIdQueryHandler.Handle [L25–L51]
      └─ maps_to DocumentLightDto [L45]
        └─ automapper.registration DataverseMappingProfile (Document->DocumentLightDto) [L400]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L45]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L43]

