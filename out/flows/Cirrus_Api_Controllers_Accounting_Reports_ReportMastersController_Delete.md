[web] DELETE /api/accounting/reports/masters/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Delete)  [L169–L176] status=200 [auth=user,admin]
  └─ calls ReportMasterRepository.Remove [L173]
  └─ calls ReportMasterRepository.WriteQuery [L172]
  └─ delete ReportMaster [L173]
    └─ reads_from ReportMasters
  └─ write ReportMaster [L172]
    └─ reads_from ReportMasters
  └─ uses_service IControlledRepository<ReportMaster>
    └─ method WriteQuery [L172]
      └─ ... (no implementation details available)

