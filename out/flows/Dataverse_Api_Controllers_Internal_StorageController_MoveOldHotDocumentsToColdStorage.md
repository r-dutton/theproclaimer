[web] PUT /api/internal/storage/move-to-cold-storage  (Dataverse.Api.Controllers.Internal.StorageController.MoveOldHotDocumentsToColdStorage)  [L234–L240] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request MoveDocumentFromHotToColdStorageCommand [L237]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.MoveDocumentFromHotToColdStorageCommandHandler.Handle [L39–L126]
      └─ uses_service UserService
        └─ method GetUserIdIfPresent [L106]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserIdIfPresent [L15-L185]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L81]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentAuditLog>
        └─ method Add [L101]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L63]
          └─ ... (no implementation details available)

