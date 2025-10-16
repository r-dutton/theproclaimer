[web] POST /api/super/users/{userId:Guid}/share-point-sync  (Dataverse.Api.Controllers.Super.Core.UsersController.ForceSyncSharePoint)  [L250–L279] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L278]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L276]
  └─ delete DataverseEntityFailureLog [L278]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L276]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L261]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L257]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L276]
      └─ ... (no implementation details available)

