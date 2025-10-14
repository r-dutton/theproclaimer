[web] PUT /api/accounting/reports/header-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Update)  [L70–L77] [auth=user,admin]
  └─ calls HeaderTemplateRepository.WriteQuery [L73]
  └─ calls ReportContentRepository.LoadReadProperties [L74]
  └─ writes_to HeaderTemplate [L73]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service IControlledRepository<HeaderTemplate>
    └─ method WriteQuery [L73]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L74]

