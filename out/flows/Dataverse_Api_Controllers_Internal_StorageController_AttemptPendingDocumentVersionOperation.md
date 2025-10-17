[web] PUT /api/internal/storage/pending-operation  (Dataverse.Api.Controllers.Internal.StorageController.AttemptPendingDocumentVersionOperation)  [L242–L250] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request PendingDocumentVersionOperationCommand -> PendingDocumentVersionOperationCommandHandler [L247]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.PendingDocumentVersionOperationCommandHandler.Handle [L24–L58]
      └─ calls DocumentVersionRepository.WriteQuery [L39]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L51]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ PendingDocumentVersionOperationCommand
    └─ handlers 1
      └─ PendingDocumentVersionOperationCommandHandler

