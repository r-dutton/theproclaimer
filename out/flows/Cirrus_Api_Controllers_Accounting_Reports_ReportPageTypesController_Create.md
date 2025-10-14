[web] POST /api/accounting/reports/pageTypes/  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Create)  [L149–L168] [auth=user,admin]
  └─ calls ReportPageTypeRepository.Add [L165]
  └─ calls ReportPageTypeRepository.ReadQuery [L156]
  └─ calls ReportPageTypeRepository.WriteQuery [L152]
  └─ queries ReportPageType [L156]
    └─ reads_from ReportPageTypes
  └─ writes_to ReportPageType [L165]
    └─ reads_from ReportPageTypes
  └─ writes_to ReportPageType [L152]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method WriteQuery [L152]

