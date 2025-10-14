[web] GET /api/accounting/ledger/journals/for-dataset/{datasetId}/report/Excel  (Cirrus.Api.Controllers.Accounting.Ledger.JournalController.GetExcelJournalReport)  [L129–L134] [auth=user]
  └─ sends_request GetJournalReportForExcelQuery [L132]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetJournalReportForExcelQueryHandler.Handle [L59–L253]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L79]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L76]

