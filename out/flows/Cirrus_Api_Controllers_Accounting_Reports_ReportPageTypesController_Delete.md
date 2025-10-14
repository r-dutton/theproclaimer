[web] DELETE /api/accounting/reports/pageTypes/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Delete)  [L203–L210] [auth=user,admin]
  └─ calls ReportContentRepository.LoadWriteProperties [L207]
  └─ calls ReportPageTypeRepository.Remove [L208]
  └─ calls ReportPageTypeRepository.WriteQuery [L206]
  └─ writes_to ReportPageType [L208]
    └─ reads_from ReportPageTypes
  └─ writes_to ReportPageType [L206]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method WriteQuery [L206]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L207]

