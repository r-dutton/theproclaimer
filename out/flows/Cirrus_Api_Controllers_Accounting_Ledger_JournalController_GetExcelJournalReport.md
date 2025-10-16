[web] GET /api/accounting/ledger/journals/for-dataset/{datasetId}/report/Excel  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetExcelJournalReport)  [L129–L134] status=200 [auth=user]
  └─ sends_request GetJournalReportForExcelQuery -> GetJournalReportForExcelQueryHandler [L132]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalReportForExcelQueryHandler.Handle [L59–L253]
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L79]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L76]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ GetJournalReportForExcelQuery
    └─ handlers 1
      └─ GetJournalReportForExcelQueryHandler

