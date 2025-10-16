[web] GET /api/internal/documents/{id:Guid}  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.GetDocumentById)  [L296–L300] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentByIdQuery -> GetDocumentByIdQueryHandler [L299]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentByIdQueryHandler.Handle [L25–L51]
      └─ maps_to DocumentLightDto [L45]
        └─ automapper.registration DataverseMappingProfile (Document->DocumentLightDto) [L401]
      └─ uses_service IControlledRepository<Document> (Scoped (inferred))
        └─ method ReadQuery [L45]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L43]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ GetDocumentByIdQuery
    └─ handlers 1
      └─ GetDocumentByIdQueryHandler
    └─ mappings 1
      └─ DocumentLightDto

