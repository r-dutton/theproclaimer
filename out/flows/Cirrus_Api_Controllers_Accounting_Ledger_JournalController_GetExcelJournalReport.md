[web] GET /api/accounting/ledger/journals/for-dataset/{datasetId}/report/Excel  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetExcelJournalReport)  [L129–L134] status=200 [auth=user]
  └─ sends_request GetJournalReportForExcelQuery [L132]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalReportForExcelQueryHandler.Handle [L59–L253]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L79]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L76]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

