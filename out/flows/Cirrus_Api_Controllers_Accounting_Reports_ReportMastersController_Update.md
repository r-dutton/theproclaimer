[web] PUT /api/accounting/reports/masters/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Update)  [L157–L164] [auth=user,admin]
  └─ calls ReportMasterRepository.WriteQuery [L160]
  └─ writes_to ReportMaster [L160]
    └─ reads_from ReportMasters
  └─ uses_service IControlledRepository<ReportMaster>
    └─ method WriteQuery [L160]

