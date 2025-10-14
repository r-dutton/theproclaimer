[web] POST /api/accounting/ledger/reports/basic-report  (Cirrus.Api.Controllers.Accounting.Ledger.LedgerReportController.GetFormattedTrialBalance)  [L37–L41] [auth=user]
  └─ sends_request GetBasicReportQuery [L40]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetBasicReportQueryHandler.Handle [L58–L159]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L81]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L87]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L79]

