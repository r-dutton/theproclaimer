[web] POST /api/internal/documents/binder-document  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateBinderDocument)  [L188–L225] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service StorageService
    └─ method UploadFile [L197]
      └─ implementation Dataverse.Services.Features.Storage.StorageService.UploadFile [L18-L331]
  └─ uses_service UserService
    └─ method GetUserId [L214]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
  └─ uses_storage StorageService.UploadFile [L197]
  └─ sends_request CreateAuditHistoryCommand [L221]
    └─ handled_by Dataverse.ApplicationService.Commands.AuditHistories.CreateAuditHistoryCommandHandler.Handle [L21–L49]
      └─ uses_service UserService
        └─ method GetUserIfPresentAsync [L36]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserIfPresentAsync [L15-L185]
      └─ uses_service IControlledRepository<AuditHistory>
        └─ method Add [L46]
          └─ ... (no implementation details available)

