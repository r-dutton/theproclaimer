[web] PUT /api/internal/storage/pending-operation  (Dataverse.Api.Controllers.Internal.StorageController.AttemptPendingDocumentVersionOperation)  [L242–L250] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request PendingDocumentVersionOperationCommand [L247]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.PendingDocumentVersionOperationCommandHandler.Handle [L24–L58]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L39]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L51]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

