[web] POST /api/ui/users/{userId:Guid}/share-point-sync  (Dataverse.Api.Controllers.UI.Core.UsersController.ForceSyncSharePoint)  [L369–L383] [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L382]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L380]
  └─ writes_to DataverseEntityFailureLog [L382]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L380]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L380]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L374]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L372]

