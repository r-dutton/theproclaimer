[web] GET /api/accounting/reports/pageTypes/{id}  (Cirrus.Api.Controllers.Accounting.Reports.ReportPageTypesController.Get)  [L53–L67] status=200 [auth=user]
  └─ maps_to ReportPageTypeDto (var dto) [L64]
  └─ calls ReportContentRepository.LoadReadProperties [L63]
  └─ calls ReportPageTypeRepository.ReadQuery [L56]
  └─ query ReportPageType [L56]
    └─ reads_from ReportPageTypes
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L63]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties [L11-L65]
  └─ sends_request PrepareImagesContentCommand [L65]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ ReportPageType writes=0 reads=1
    └─ services 1
      └─ ReportContentRepository
    └─ requests 1
      └─ PrepareImagesContentCommand
    └─ mappings 1
      └─ ReportPageTypeDto

