[web] DELETE /api/ui/clients/{id}  (Dataverse.Api.Controllers.UI.Core.ClientsController.Delete)  [L359–L367] status=200 [auth=Authentication.UserPolicy]
  └─ uses_client ClientRepository [L362]
  └─ sends_request CanIAccessClientQuery -> CanIAccessClientQueryHandler [L365]
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
    └─ clients 1
      └─ ClientRepository
    └─ requests 1
      └─ CanIAccessClientQuery
    └─ handlers 1
      └─ CanIAccessClientQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

