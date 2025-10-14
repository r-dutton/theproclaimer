[web] POST /api/accounting/reports/header-templates/  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Create)  [L62–L68] [auth=user,admin]
  └─ calls HeaderTemplateRepository.Add [L66]
  └─ writes_to HeaderTemplate [L66]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service IControlledRepository<HeaderTemplate>
    └─ method Add [L66]

