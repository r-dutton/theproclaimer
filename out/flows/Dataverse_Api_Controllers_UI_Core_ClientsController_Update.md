[web] PUT /api/ui/clients/{id}  (Dataverse.Api.Controllers.UI.Core.ClientsController.Update)  [L273–L310] status=200 [auth=Authentication.UserPolicy]
  └─ uses_client ClientRepository [L286]
  └─ uses_client ClientRepository [L276]
  └─ calls OfficeRepository.ReadQuery [L291]
  └─ query Office [L291]
    └─ reads_from Offices
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L301]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L291]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L281]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
        └─ uses_client WorkpapersClient [L78]
        └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
          └─ method GetCurrentSettingsAsync [L52]
            └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.GetCurrentSettingsAsync
        └─ uses_service TenantService
          └─ method GetCurrentSettingsAsync [L44]
            └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentSettingsAsync [L6-L27]
              └─ uses_service TenantIdentificationService
                └─ method GetCurrentTenant [L20]
                  └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
        └─ uses_cache IDistributedCache.GetRecordAsync [read] [L70]
        └─ uses_cache IDistributedCache.RemoveAsync [write] [L46]
  └─ sends_request CanIAccessClientQuery -> CanIAccessClientQueryHandler [L280]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessClientQueryHandler.Handle [L41–L104]
      └─ uses_service IControlledRepository<Client> (Scoped (inferred))
        └─ method ReadQuery [L85]
          └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.ReadQuery
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L83]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
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
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L71]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L71]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method GetCatalogByDataverseId [L111]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId (see previous expansion)
            └─ uses_service Tenant
              └─ method AssignCurrentTenantId [L80]
                └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId (see previous expansion)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L64]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L98]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L73]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L71]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ clients 2
      └─ ClientRepository
      └─ WorkpapersClient
    └─ services 3
      └─ FirmSettingsService
      └─ OfficeRepository
      └─ TenantService
    └─ requests 1
      └─ CanIAccessClientQuery
    └─ handlers 1
      └─ CanIAccessClientQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

