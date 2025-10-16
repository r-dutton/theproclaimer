[web] POST /api/accounting/reports/pageTypes/  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Create)  [L149–L168] status=201 [auth=user,admin]
  └─ calls ReportPageTypeRepository (methods: Add,ReadQuery,WriteQuery) [L165]
  └─ insert ReportPageType [L165]
    └─ reads_from ReportPageTypes
  └─ query ReportPageType [L156]
    └─ reads_from ReportPageTypes
  └─ write ReportPageType [L152]
    └─ reads_from ReportPageTypes
  └─ impact_summary
    └─ entities 1 (writes=2, reads=1)
      └─ ReportPageType writes=2 reads=1

