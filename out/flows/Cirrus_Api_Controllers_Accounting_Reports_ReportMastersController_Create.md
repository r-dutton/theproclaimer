[web] POST /api/accounting/reports/masters/  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Create)  [L135–L142] [auth=user,admin]
  └─ calls ReportMasterRepository.Add [L140]
  └─ writes_to ReportMaster [L140]
    └─ reads_from ReportMasters
  └─ uses_service IControlledRepository<ReportMaster>
    └─ method Add [L140]

