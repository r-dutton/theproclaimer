[web] POST /api/super/offices/{officeId:Guid}/share-point-sync  (Dataverse.Api.Controllers.Super.Core.OfficesController.ForceSyncSharePoint)  [L132–L146] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L145]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L143]
  └─ writes_to DataverseEntityFailureLog [L145]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L143]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L143]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L137]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L135]

