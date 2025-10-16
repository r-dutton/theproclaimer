[web] POST /api/internal/documents/{documentId:guid}/versions/  (Dataverse.Api.Controllers.Internal.Documents.DocumentVersionsController.AddDocumentVersion)  [L29–L34] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request AddDocumentVersionCommand [L33]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.AddDocumentVersionCommandHandler.Handle [L27–L94]
      └─ uses_service UserService
        └─ method GetUserId [L66]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service RequestInfoService
        └─ method IsM2MRequest [L67]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsM2MRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsM2MRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsM2MRequest [L11-L92]
      └─ uses_service IControlledRepository<Document>
        └─ method WriteQuery [L57]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentStatus>
        └─ method WriteQuery [L78]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method ReadQuery [L61]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L88]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

