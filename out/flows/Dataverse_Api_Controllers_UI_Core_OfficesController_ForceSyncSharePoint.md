[web] POST /api/ui/offices/{officeId:Guid}/share-point-sync  (Dataverse.Api.Controllers.UI.Core.OfficesController.ForceSyncSharePoint)  [L274–L288] [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L287]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L285]
  └─ writes_to DataverseEntityFailureLog [L287]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L285]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L285]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L279]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L277]

