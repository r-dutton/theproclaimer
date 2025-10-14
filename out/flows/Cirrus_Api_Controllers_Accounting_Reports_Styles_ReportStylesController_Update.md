[web] PUT /api/accounting/reports/styles/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Styles.ReportStylesController.Update)  [L84–L95] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls ReportStyleRepository.WriteQuery [L87]
  └─ writes_to ReportStyle [L87]
    └─ reads_from ReportStyles
  └─ uses_service IControlledRepository<ReportStyle>
    └─ method WriteQuery [L87]
  └─ sends_request ValidateCssFontUrlsCommand [L91]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Reports.ReportStyles.ValidateCssFontUrlsCommandHandler.Handle [L24–L60]
      └─ uses_service IOptions<CssTemplatesConfig>
        └─ method Value [L34]

