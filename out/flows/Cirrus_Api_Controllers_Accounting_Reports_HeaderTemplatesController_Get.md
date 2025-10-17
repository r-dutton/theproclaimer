[web] GET /api/accounting/reports/header-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Get)  [L39–L49] status=200 [auth=user]
  └─ maps_to HeaderTemplateDto (var dto) [L46]
  └─ calls ReportContentRepository.LoadReadProperties [L45]
  └─ calls HeaderTemplateRepository.ReadQuery [L42]
  └─ query HeaderTemplate [L42]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L45]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties [L11-L65]
  └─ sends_request PrepareImagesContentCommand [L47]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ HeaderTemplate writes=0 reads=1
    └─ services 1
      └─ ReportContentRepository
    └─ requests 1
      └─ PrepareImagesContentCommand
    └─ mappings 1
      └─ HeaderTemplateDto

