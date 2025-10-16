[web] GET /api/ui/audit-histories/search  (Dataverse.Api.Controllers.UI.AuditHistories.AuditHistoriesController.Search)  [L26–L55] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindAuditHistoriesQuery [L40]
    └─ handled_by Dataverse.ApplicationService.Queries.AuditHistories.FindAuditHistoriesQueryHandler.Handle [L55–L89]
      └─ uses_service IControlledRepository<AuditHistory>
        └─ method ReadQuery [L70]
          └─ ... (no implementation details available)

