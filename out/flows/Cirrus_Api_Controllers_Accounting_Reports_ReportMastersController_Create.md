[web] POST /api/accounting/reports/masters/  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Create)  [L135–L142] status=201 [auth=user,admin]
  └─ calls ReportMasterRepository.Add [L140]
  └─ insert ReportMaster [L140]
    └─ reads_from ReportMasters
  └─ uses_service IControlledRepository<ReportMaster>
    └─ method Add [L140]
      └─ ... (no implementation details available)

