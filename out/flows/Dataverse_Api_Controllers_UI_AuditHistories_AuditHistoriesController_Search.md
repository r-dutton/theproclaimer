[web] GET /api/ui/audit-histories/search  (Dataverse.Api.Controllers.UI.AuditHistories.AuditHistoriesController.Search)  [L26–L55] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindAuditHistoriesQuery -> FindAuditHistoriesQueryHandler [L40]
    └─ handled_by Dataverse.ApplicationService.Queries.AuditHistories.FindAuditHistoriesQueryHandler.Handle [L55–L89]
      └─ calls AuditHistoryRepository.ReadQuery [L70]
  └─ impact_summary
    └─ requests 1
      └─ FindAuditHistoriesQuery
    └─ handlers 1
      └─ FindAuditHistoriesQueryHandler

