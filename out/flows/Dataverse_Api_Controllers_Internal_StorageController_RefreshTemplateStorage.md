[web] GET /api/internal/storage/refresh-template-storage  (Dataverse.Api.Controllers.Internal.StorageController.RefreshTemplateStorage)  [L256–L262] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service ITemplateService (AddSingleton)
    └─ method GetTemplates [L259]
      └─ implementation Dataverse.Templates.TemplateService.GetTemplates [L17-L359]
        └─ uses_service TemplateConfiguration
          └─ method GetSiteDriveId [L333]
            └─ ... (no implementation details available)
        └─ uses_service SharePointTemplateManager
          └─ method GetDownloadUrl [L82]
            └─ ... (no implementation details available)
        └─ uses_cache IDistributedCache.SetRecordAsync [write] [L47]
        └─ uses_cache IDistributedCache.GetRecordAsync [read] [L41]
  └─ impact_summary
    └─ services 1
      └─ ITemplateService
    └─ caches 1
      └─ IDistributedCache

