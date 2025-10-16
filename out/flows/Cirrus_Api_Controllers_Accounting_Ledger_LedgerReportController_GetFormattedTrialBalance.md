[web] POST /api/accounting/ledger/reports/basic-report  (Cirrus.Api.Controllers.Accounting.Ledger.LedgerReportController.GetFormattedTrialBalance)  [L37–L41] status=201 [auth=user]
  └─ sends_request GetBasicReportQuery -> GetBasicReportQueryHandler [L40]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Reports.GetBasicReportQueryHandler.Handle [L58–L159]
      └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
        └─ method ReadQuery [L87]
          └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L81]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L79]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ GetBasicReportQuery
    └─ handlers 1
      └─ GetBasicReportQueryHandler

