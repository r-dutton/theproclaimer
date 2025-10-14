[web] POST /api/internal/audit-histories  (Dataverse.Api.Controllers.Internal.AuditHistories.AuditHistoriesController.CreateAuditHistoryEntry)  [L27–L34] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateAuditHistoryCommand [L31]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.AuditHistories.CreateAuditHistoryCommandHandler.Handle [L21–L49]
      └─ uses_service IControlledRepository<AuditHistory>
        └─ method Add [L46]
      └─ uses_service UserService
        └─ method GetUserIfPresentAsync [L36]

