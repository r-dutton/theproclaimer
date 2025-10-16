[web] POST /api/accounting/ledger/reports/basic-report  (Cirrus.Api.Controllers.Accounting.Ledger.LedgerReportController.GetFormattedTrialBalance)  [L37–L41] status=201 [auth=user]
  └─ sends_request GetBasicReportQuery [L40]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetBasicReportQueryHandler.Handle [L58–L159]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L81]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L87]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L79]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

