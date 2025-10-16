[web] DELETE /api/accounting/reports/header-templates/{id}  (Cirrus.Api.Controllers.Accounting.Reports.HeaderTemplatesController.Delete)  [L79–L85] status=200 [auth=user,admin]
  └─ calls HeaderTemplateRepository.Remove [L83]
  └─ calls HeaderTemplateRepository.WriteQuery [L82]
  └─ delete HeaderTemplate [L83]
    └─ reads_from ReportHeaderTemplates
  └─ write HeaderTemplate [L82]
    └─ reads_from ReportHeaderTemplates
  └─ uses_service IControlledRepository<HeaderTemplate>
    └─ method WriteQuery [L82]
      └─ ... (no implementation details available)

