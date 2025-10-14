[web] GET /api/accounting/reports/masters  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetReportMasterParameters)  [L178–L189] [auth=user]
  └─ calls ReportPageTypeRepository.WriteQuery [L185]
  └─ writes_to ReportPageType [L185]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method WriteQuery [L185]

