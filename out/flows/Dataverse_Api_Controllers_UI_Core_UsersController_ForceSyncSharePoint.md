[web] POST /api/ui/users/{userId:Guid}/share-point-sync  (Dataverse.Api.Controllers.UI.Core.UsersController.ForceSyncSharePoint)  [L369–L383] status=201 [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L382]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L380]
  └─ delete DataverseEntityFailureLog [L382]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L380]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L374]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L372]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L380]
      └─ ... (no implementation details available)

