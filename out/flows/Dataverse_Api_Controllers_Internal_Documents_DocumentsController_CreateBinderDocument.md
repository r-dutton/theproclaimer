[web] POST /api/internal/documents/binder-document  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateBinderDocument)  [L188–L225] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service StorageService
    └─ method UploadFile [L197]
  └─ uses_service UserService
    └─ method GetUserId [L214]
  └─ sends_request CreateAuditHistoryCommand [L221]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AuditHistories.CreateAuditHistoryCommandHandler.Handle [L21–L49]
      └─ uses_service IControlledRepository<AuditHistory>
        └─ method Add [L46]
      └─ uses_service UserService
        └─ method GetUserIfPresentAsync [L36]

