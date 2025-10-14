[web] GET /api/journals/for-binder-column/{binderColumnId:guid}/report  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetJournalReport)  [L164–L168] [auth=AuthorizationPolicies.User]
  └─ sends_request GetJournalReportQuery [L167]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalReportQueryHandler.Handle [L42–L90]
      └─ uses_service GetJournalsWithTransactionsQuery
        └─ method Execute [L72]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L63]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L57]

