[web] GET /api/accounting/reports/header-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Get)  [L39–L49] [auth=user]
  └─ maps_to HeaderTemplateDto (var dto) [L46]
  └─ calls HeaderTemplateRepository.ReadQuery [L42]
  └─ calls ReportContentRepository.LoadReadProperties [L45]
  └─ queries HeaderTemplate [L42]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service IControlledRepository<HeaderTemplate>
    └─ method ReadQuery [L42]
  └─ uses_service IMapper
    └─ method Map [L46]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L45]
  └─ sends_request PrepareImagesContentCommand [L47]

