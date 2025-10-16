[web] GET /api/ui/users/export/excel/download  (Dataverse.Api.Controllers.UI.Core.UsersController.ExportToExcel)  [L179–L192] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to UserExportProfileDto [L182]
    └─ automapper.registration DataverseMappingProfile (User->UserExportProfileDto) [L109]
  └─ calls UserRepository.ReadQuery [L182]
  └─ query User [L182]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L188]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L182]
      └─ ... (no implementation details available)
  └─ sends_request CreateDownloadCommand [L190]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation IStorageService.CreateDownloadUrl [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]
  └─ sends_request ExportUsersToExcelCommand [L189]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ExportUsersToExcelCommandHandler.Handle [L24–L42]
      └─ uses_service UserExcelWriter
        └─ method WriteAsync [L38]
          └─ ... (no implementation details available)

