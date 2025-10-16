[web] DELETE /api/accounting/reports/masters/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Delete)  [L169–L176] status=200 [auth=user,admin]
  └─ calls ReportMasterRepository (methods: Remove,WriteQuery) [L173]
  └─ delete ReportMaster [L173]
    └─ reads_from ReportMasters
  └─ write ReportMaster [L172]
    └─ reads_from ReportMasters
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ ReportMaster writes=2 reads=0

