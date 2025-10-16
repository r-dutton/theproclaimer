[web] POST /api/entities/  (Cirrus.Api.Controllers.Firm.EntitiesController.Create)  [L94–L106] status=201 [auth=user]
  └─ calls EntityRepository.Add [L104]
  └─ calls ClientRepository.WriteQuery [L97]
  └─ insert Entity [L104]
  └─ write Client [L97]
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetFirmJurisdictions [L100]
      └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetFirmJurisdictions [L15-L112]
        └─ uses_service IRequestProcessor (InstancePerDependency)
          └─ method GetCurrentSettings [L45]
            └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCurrentSettings [L7-L35]
        └─ uses_service TenantService
          └─ method GetCurrentSettings [L34]
            └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentSettings [L26-L139]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method GetCatalogByDataverseId [L111]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
              └─ uses_service Tenant
                └─ method AssignCurrentTenantId [L80]
                  └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
        └─ uses_cache IDistributedCache.SetStringAsync [write] [L108]
        └─ uses_cache IDistributedCache.GetStringAsync [read] [L98]
  └─ impact_summary
    └─ entities 2 (writes=2, reads=0)
      └─ Client writes=1 reads=0
      └─ Entity writes=1 reads=0
    └─ services 1
      └─ IFirmSettingsService
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

