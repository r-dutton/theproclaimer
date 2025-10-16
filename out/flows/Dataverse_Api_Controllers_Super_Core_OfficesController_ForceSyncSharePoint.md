[web] POST /api/super/offices/{officeId:Guid}/share-point-sync  (Dataverse.Api.Controllers.Super.Core.OfficesController.ForceSyncSharePoint)  [L132–L146] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L145]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L143]
  └─ delete DataverseEntityFailureLog [L145]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L143]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L137]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L135]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L143]
      └─ ... (no implementation details available)

