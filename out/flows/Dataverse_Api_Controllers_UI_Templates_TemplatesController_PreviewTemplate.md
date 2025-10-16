[web] GET /api/ui/templates/{id}/preview  (Dataverse.Api.Controllers.UI.Templates.TemplatesController.PreviewTemplate)  [L76–L84] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service ITemplateService (AddSingleton)
    └─ method GetTemplatePreviewUrl [L81]
      └─ implementation Dataverse.Templates.TemplateService.GetTemplatePreviewUrl [L17-L359]

