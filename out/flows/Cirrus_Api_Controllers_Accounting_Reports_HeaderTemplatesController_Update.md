[web] PUT /api/accounting/reports/header-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Update)  [L70–L77] status=200 [auth=user,admin]
  └─ calls HeaderTemplateRepository.WriteQuery [L73]
  └─ calls ReportContentRepository.LoadReadProperties [L74]
  └─ write HeaderTemplate [L73]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service IControlledRepository<HeaderTemplate>
    └─ method WriteQuery [L73]
      └─ ... (no implementation details available)
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L74]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties [L11-L65]

