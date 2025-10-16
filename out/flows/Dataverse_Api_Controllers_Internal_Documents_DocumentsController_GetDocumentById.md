[web] GET /api/internal/documents/{id:Guid}  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.GetDocumentById)  [L296–L300] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentByIdQuery [L299]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentByIdQueryHandler.Handle [L25–L51]
      └─ maps_to DocumentLightDto [L45]
        └─ automapper.registration DataverseMappingProfile (Document->DocumentLightDto) [L401]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L45]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L43]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

