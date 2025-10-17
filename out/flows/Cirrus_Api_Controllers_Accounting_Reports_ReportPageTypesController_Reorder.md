[web] PUT /api/accounting/reports/pageTypes/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Reorder)  [L186–L201] status=200 [auth=user,admin]
  └─ calls ReportPageTypeRepository (methods: WriteQuery,ReadQuery) [L193]
  └─ write ReportPageType [L193]
    └─ reads_from ReportPageTypes
  └─ query ReportPageType [L189]
    └─ reads_from ReportPageTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ ReportPageType writes=1 reads=1

