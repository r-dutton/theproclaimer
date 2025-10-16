[web] GET /api/journals/for-binder-column/{binderColumnId:guid}/report  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetJournalReport)  [L164–L168] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetJournalReportQuery [L167]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalReportQueryHandler.Handle [L42–L90]
      └─ uses_service GetJournalsWithTransactionsQuery
        └─ method Execute [L72]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L63]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L57]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

