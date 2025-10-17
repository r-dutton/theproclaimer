[web] GET /api/accounting/reports/masters  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetReportMasterParameters)  [L178–L189] status=200 [auth=user]
  └─ calls ReportPageTypeRepository.WriteQuery [L185]
  └─ write ReportPageType [L185]
    └─ reads_from ReportPageTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ ReportPageType writes=1 reads=0

