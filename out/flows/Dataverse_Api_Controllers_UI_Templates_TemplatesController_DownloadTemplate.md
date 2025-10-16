[web] GET /api/ui/templates/{id}/download  (Dataverse.Api.Controllers.UI.Templates.TemplatesController.DownloadTemplate)  [L66–L74] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service ITemplateService (AddSingleton)
    └─ method GetTemplateDownloadUrl [L71]
      └─ implementation Dataverse.Templates.TemplateService.GetTemplateDownloadUrl [L17-L359]

