[web] PUT /api/accounting/reports/pageTypes/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Reorder)  [L186–L201] status=200 [auth=user,admin]
  └─ calls ReportPageTypeRepository.WriteQuery [L193]
  └─ calls ReportPageTypeRepository.ReadQuery [L189]
  └─ query ReportPageType [L189]
    └─ reads_from ReportPageTypes
  └─ write ReportPageType [L193]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method ReadQuery [L189]
      └─ ... (no implementation details available)

