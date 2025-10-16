[web] GET /api/internal/storage/refresh-template-storage  (Dataverse.Api.Controllers.Internal.StorageController.RefreshTemplateStorage)  [L256–L262] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service ITemplateService (AddSingleton)
    └─ method GetTemplates [L259]
      └─ implementation Dataverse.Templates.TemplateService.GetTemplates [L17-L359]

