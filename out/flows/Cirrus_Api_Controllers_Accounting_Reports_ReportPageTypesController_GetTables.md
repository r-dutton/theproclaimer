[web] POST /api/accounting/reports/pageTypes/tables  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.GetTables)  [L97–L118] [auth=user]
  └─ calls ReportPageTypeRepository.ReadQuery [L100]
  └─ queries ReportPageType [L100]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method ReadQuery [L100]

