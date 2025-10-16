[web] PUT /api/accounting/reports/header-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Update)  [L70–L77] status=200 [auth=user,admin]
  └─ calls ReportContentRepository.LoadReadProperties [L74]
  └─ calls HeaderTemplateRepository.WriteQuery [L73]
  └─ write HeaderTemplate [L73]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L74]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties [L11-L65]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ HeaderTemplate writes=1 reads=0
    └─ services 1
      └─ ReportContentRepository

