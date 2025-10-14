[web] PUT /api/accounting/reports/pageTypes/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Update)  [L170–L177] [auth=user,admin]
  └─ calls ReportContentRepository.LoadWriteProperties [L174]
  └─ calls ReportPageTypeRepository.WriteQuery [L173]
  └─ writes_to ReportPageType [L173]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method WriteQuery [L173]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L174]

