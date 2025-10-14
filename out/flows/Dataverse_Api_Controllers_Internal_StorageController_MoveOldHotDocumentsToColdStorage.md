[web] PUT /api/internal/storage/move-to-cold-storage  (Dataverse.Api.Controllers.Internal.StorageController.MoveOldHotDocumentsToColdStorage)  [L234–L240] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request MoveDocumentFromHotToColdStorageCommand [L237]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.MoveDocumentFromHotToColdStorageCommandHandler.Handle [L39–L126]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L81]
      └─ uses_service IControlledRepository<DocumentAuditLog>
        └─ method Add [L101]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L63]
      └─ uses_service UserService
        └─ method GetUserIdIfPresent [L106]

