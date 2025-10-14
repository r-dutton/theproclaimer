[web] POST /api/super/users/{userId:Guid}/share-point-sync  (Dataverse.Api.Controllers.Super.Core.UsersController.ForceSyncSharePoint)  [L250–L279] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L278]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L276]
  └─ writes_to DataverseEntityFailureLog [L278]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L276]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L276]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L261]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L257]

