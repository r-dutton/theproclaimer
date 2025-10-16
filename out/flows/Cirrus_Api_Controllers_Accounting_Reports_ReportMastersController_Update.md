[web] PUT /api/accounting/reports/masters/{id:int}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.Update)  [L157–L164] status=200 [auth=user,admin]
  └─ calls ReportMasterRepository.WriteQuery [L160]
  └─ write ReportMaster [L160]
    └─ reads_from ReportMasters
  └─ uses_service IControlledRepository<ReportMaster>
    └─ method WriteQuery [L160]
      └─ ... (no implementation details available)

