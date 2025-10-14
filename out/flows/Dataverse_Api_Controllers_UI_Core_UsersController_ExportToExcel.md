[web] GET /api/ui/users/export/excel/download  (Dataverse.Api.Controllers.UI.Core.UsersController.ExportToExcel)  [L179–L192] [auth=Authentication.UserPolicy]
  └─ maps_to UserExportProfileDto [L182]
    └─ automapper.registration DataverseMappingProfile (User->UserExportProfileDto) [L108]
  └─ calls UserRepository.ReadQuery [L182]
  └─ queries User [L182]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L182]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L188]
  └─ sends_request CreateDownloadCommand [L190]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
  └─ sends_request ExportUsersToExcelCommand [L189]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ExportUsersToExcelCommandHandler.Handle [L24–L42]
      └─ uses_service UserExcelWriter
        └─ method WriteAsync [L38]

