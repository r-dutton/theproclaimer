[web] GET /api/accounting/reports/pageTypes/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Get)  [L53–L67] [auth=user]
  └─ maps_to ReportPageTypeDto (var dto) [L64]
  └─ calls ReportContentRepository.LoadReadProperties [L63]
  └─ calls ReportPageTypeRepository.ReadQuery [L56]
  └─ queries ReportPageType [L56]
    └─ reads_from ReportPageTypes
  └─ uses_service IControlledRepository<ReportPageType>
    └─ method ReadQuery [L56]
  └─ uses_service IMapper
    └─ method Map [L64]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L63]
  └─ sends_request PrepareImagesContentCommand [L65]

