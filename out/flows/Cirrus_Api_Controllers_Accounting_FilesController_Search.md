[web] GET /api/accounting/files/  (Cirrus.Api.Controllers.Accounting.FilesController.Search)  [L55–L81] status=200 [auth=user]
  └─ sends_request FindFilesQuery -> FindFilesQueryHandler [L69]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindFilesQueryHandler.Handle [L58–L195]
      └─ maps_to UserLightDto [L181]
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L172]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IControlledRepository<User> (Scoped (inferred))
        └─ method ReadQuery [L125]
          └─ implementation Cirrus.Data.Repository.Firm.UserRepository.ReadQuery
      └─ uses_service IFirmSettingsService (AddScoped)
        └─ method GetCurrentSettings [L122]
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
      └─ uses_service IControlledRepository<File> (Scoped (inferred))
        └─ method ReadQuery [L91]
          └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L87]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ requests 1
      └─ FindFilesQuery
    └─ handlers 1
      └─ FindFilesQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ UserLightDto

