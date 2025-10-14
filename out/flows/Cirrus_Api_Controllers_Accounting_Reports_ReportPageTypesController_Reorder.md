[web] PUT /api/accounting/reports/pageTypes/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Reorder)  [L186–L201] [auth=user,admin]
  └─ calls ReportPageTypeRepository.WriteQuery [L193]
  └─ calls ReportPageTypeRepository.ReadQuery [L189]
  └─ queries ReportPageType [L189]
    └─ reads_from ReportPageTypes
  └─ writes_to ReportPageType [L193]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method ReadQuery [L189]

