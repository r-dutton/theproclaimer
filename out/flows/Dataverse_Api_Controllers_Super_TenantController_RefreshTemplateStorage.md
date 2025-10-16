[web] POST /api/super/tenants/refresh-template-storage  (Dataverse.Api.Controllers.Super.TenantController.RefreshTemplateStorage)  [L175–L180] status=201 [auth=Authentication.MasterPolicy]
  └─ uses_service ITemplateService (AddSingleton)
    └─ method GetTemplates [L178]
      └─ implementation Dataverse.Templates.TemplateService.GetTemplates [L17-L359]

