[web] POST /api/accounting/reports/masters/  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Create)  [L135–L142] status=201 [auth=user,admin]
  └─ calls ReportMasterRepository.Add [L140]
  └─ insert ReportMaster [L140]
    └─ reads_from ReportMasters
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ ReportMaster writes=1 reads=0

