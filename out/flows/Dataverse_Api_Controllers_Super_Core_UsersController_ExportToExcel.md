[web] GET /api/super/users/export/excel/download  (Dataverse.Api.Controllers.Super.Core.UsersController.ExportToExcel)  [L281–L296] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserExportProfileDto [L285]
    └─ automapper.registration DataverseMappingProfile (User->UserExportProfileDto) [L108]
  └─ calls UserRepository.ReadQuery [L285]
  └─ queries User [L285]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L285]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L291]
  └─ sends_request CreateDownloadCommand [L294]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
  └─ sends_request ExportUsersToExcelCommand [L292]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ExportUsersToExcelCommandHandler.Handle [L24–L42]
      └─ uses_service UserExcelWriter
        └─ method WriteAsync [L38]

