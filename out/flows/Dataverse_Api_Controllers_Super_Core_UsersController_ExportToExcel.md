[web] GET /api/super/users/export/excel/download  (Dataverse.Api.Controllers.Super.Core.UsersController.ExportToExcel)  [L281–L296] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserExportProfileDto [L285]
    └─ automapper.registration DataverseMappingProfile (User->UserExportProfileDto) [L109]
  └─ calls UserRepository.ReadQuery [L285]
  └─ query User [L285]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L291]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L285]
      └─ ... (no implementation details available)
  └─ sends_request CreateDownloadCommand [L294]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation IStorageService.CreateDownloadUrl [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]
  └─ sends_request ExportUsersToExcelCommand [L292]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ExportUsersToExcelCommandHandler.Handle [L24–L42]
      └─ uses_service UserExcelWriter
        └─ method WriteAsync [L38]
          └─ ... (no implementation details available)

