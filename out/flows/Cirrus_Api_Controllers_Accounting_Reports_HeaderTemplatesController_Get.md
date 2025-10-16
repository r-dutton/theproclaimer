[web] GET /api/accounting/reports/header-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Get)  [L39–L49] status=200 [auth=user]
  └─ maps_to HeaderTemplateDto (var dto) [L46]
  └─ calls HeaderTemplateRepository.ReadQuery [L42]
  └─ calls ReportContentRepository.LoadReadProperties [L45]
  └─ query HeaderTemplate [L42]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service IControlledRepository<HeaderTemplate>
    └─ method ReadQuery [L42]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L46]
      └─ ... (no implementation details available)
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L45]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties [L11-L65]
  └─ sends_request PrepareImagesContentCommand [L47]

