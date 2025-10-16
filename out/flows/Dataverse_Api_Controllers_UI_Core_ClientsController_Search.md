[web] GET /api/ui/clients/  (Dataverse.Api.Controllers.UI.Core.ClientsController.Search)  [L86–L109] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindClientsQuery -> FindClientsQueryHandler [L108]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindClientsQueryHandler.Handle [L76–L187]
      └─ uses_service IControlledRepository<Office> (Scoped (inferred))
        └─ method ReadQuery [L115]
          └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L108]
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
        └─ method GetUserId [L99]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IControlledRepository<Client> (Scoped (inferred))
        └─ method ReadQuery [L96]
          └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.ReadQuery
  └─ sends_request FindClientsLightQuery [L107]
  └─ impact_summary
    └─ requests 2
      └─ FindClientsLightQuery
      └─ FindClientsQuery
    └─ handlers 1
      └─ FindClientsQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

