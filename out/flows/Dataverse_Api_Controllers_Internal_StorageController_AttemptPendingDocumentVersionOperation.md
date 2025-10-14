[web] PUT /api/internal/storage/pending-operation  (Dataverse.Api.Controllers.Internal.StorageController.AttemptPendingDocumentVersionOperation)  [L242–L250] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request PendingDocumentVersionOperationCommand [L247]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.PendingDocumentVersionOperationCommandHandler.Handle [L24–L58]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L39]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L51]

