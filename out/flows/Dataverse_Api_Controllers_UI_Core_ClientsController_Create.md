[web] POST /api/ui/clients/  (Dataverse.Api.Controllers.UI.Core.ClientsController.Create)  [L229–L271] status=201 [auth=Authentication.UserPolicy]
  └─ uses_client ClientRepository [L268]
  └─ uses_client ClientRepository [L253]
  └─ calls OfficeRepository.ReadQuery [L237]
  └─ query Office [L237]
    └─ reads_from Offices
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L261]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L248]
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
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L237]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ sends_request CanIAccessTeamQuery -> CanIAccessTeamQueryHandler [L245]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessTeamQueryHandler.Handle [L39–L110]
      └─ uses_service IControlledRepository<TeamUser> (Scoped (inferred))
        └─ method ReadQuery [L86]
          └─ implementation Cirrus.Data.Repository.Firm.TeamUserRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L66]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method GetCatalogByDataverseId [L111]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
            └─ uses_service Tenant
              └─ method AssignCurrentTenantId [L80]
                └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L65]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L60]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L103]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L69]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L66]
  └─ sends_request CanIAccessOfficeQuery -> CanIAccessOfficeQueryHandler [L236]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessOfficeQueryHandler.Handle [L39–L110]
      └─ uses_service IControlledRepository<OfficeUser> (Scoped (inferred))
        └─ method ReadQuery [L86]
          └─ implementation Cirrus.Data.Repository.Firm.OfficeUserRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L66]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant (see previous expansion)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L65]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L60]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L103]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L69]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L66]
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
    └─ requests 2
      └─ CanIAccessOfficeQuery
      └─ CanIAccessTeamQuery
    └─ handlers 2
      └─ CanIAccessOfficeQueryHandler
      └─ CanIAccessTeamQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

