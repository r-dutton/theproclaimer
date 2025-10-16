[web] POST /api/accounting/reports/pageTypes/  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Create)  [L149–L168] status=201 [auth=user,admin]
  └─ calls ReportPageTypeRepository.Add [L165]
  └─ calls ReportPageTypeRepository.ReadQuery [L156]
  └─ calls ReportPageTypeRepository.WriteQuery [L152]
  └─ insert ReportPageType [L165]
    └─ reads_from ReportPageTypes
  └─ query ReportPageType [L156]
    └─ reads_from ReportPageTypes
  └─ write ReportPageType [L152]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method WriteQuery [L152]
      └─ ... (no implementation details available)

