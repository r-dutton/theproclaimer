[web] POST /api/audit-trail/binders/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.AuditHistoriesController.CreateAuditMessageForBinder)  [L46–L52] [auth=AuthorizationPolicies.User]
  └─ sends_request CreateAuditMessageForBinderCommand [L49]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.AuditHistories.CreateAuditMessageForBinderCommandHandler.Handle [L91–L138]
      └─ uses_service AuditHistoryStorageService
        └─ method QueueLog [L135]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L110]
      └─ uses_service UserService
        └─ method GetUserIdIfPresent [L116]

