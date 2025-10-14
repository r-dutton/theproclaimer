[web] GET /api/ui/audit-histories/search  (Dataverse.Api.Controllers.UI.AuditHistories.AuditHistoriesController.Search)  [L26–L55] [auth=Authentication.UserPolicy]
  └─ sends_request FindAuditHistoriesQuery [L40]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.AuditHistories.FindAuditHistoriesQueryHandler.Handle [L55–L89]
      └─ uses_service IControlledRepository<AuditHistory>
        └─ method ReadQuery [L70]

