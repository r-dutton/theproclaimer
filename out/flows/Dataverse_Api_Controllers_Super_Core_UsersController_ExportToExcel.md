[web] GET /api/super/users/export/excel/download  (Dataverse.Api.Controllers.Super.Core.UsersController.ExportToExcel)  [L281–L296] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserExportProfileDto [L285]
    └─ automapper.registration DataverseMappingProfile (User->UserExportProfileDto) [L109]
  └─ calls UserRepository.ReadQuery [L285]
  └─ query User [L285]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L291]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ uses_service UserRepository
    └─ method ReadQuery [L285]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L294]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation DataGet.Data.BlobStorage.StorageService.CreateDownloadUrl [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]
  └─ sends_request ExportUsersToExcelCommand -> ExportUsersToExcelCommandHandler [L292]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ExportUsersToExcelCommandHandler.Handle [L24–L42]
      └─ uses_service UserExcelWriter
        └─ method WriteAsync [L38]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 2
      └─ TenantService
      └─ UserRepository
    └─ requests 2
      └─ CreateDownloadCommand
      └─ ExportUsersToExcelCommand
    └─ handlers 2
      └─ CreateDownloadCommandHandler
      └─ ExportUsersToExcelCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ UserExportProfileDto

