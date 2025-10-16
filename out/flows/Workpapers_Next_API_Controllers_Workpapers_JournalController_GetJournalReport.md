[web] GET /api/journals/for-binder-column/{binderColumnId:guid}/report  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetJournalReport)  [L164–L168] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetJournalReportQuery -> GetJournalReportQueryHandler [L167]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalReportQueryHandler.Handle [L42–L90]
      └─ uses_service GetJournalsWithTransactionsQuery
        └─ method Execute [L72]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L63]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L57]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ GetJournalReportQuery
    └─ handlers 1
      └─ GetJournalReportQueryHandler

