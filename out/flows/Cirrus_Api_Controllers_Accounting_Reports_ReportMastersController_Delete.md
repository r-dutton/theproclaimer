[web] DELETE /api/accounting/reports/masters/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Delete)  [L169–L176] [auth=user,admin]
  └─ calls ReportMasterRepository.Remove [L173]
  └─ calls ReportMasterRepository.WriteQuery [L172]
  └─ writes_to ReportMaster [L173]
    └─ reads_from ReportMasters
  └─ writes_to ReportMaster [L172]
    └─ reads_from ReportMasters
  └─ uses_service IControlledRepository<ReportMaster>
    └─ method WriteQuery [L172]

