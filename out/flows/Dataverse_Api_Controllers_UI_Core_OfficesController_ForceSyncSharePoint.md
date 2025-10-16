[web] POST /api/ui/offices/{officeId:Guid}/share-point-sync  (Dataverse.Api.Controllers.UI.Core.OfficesController.ForceSyncSharePoint)  [L274–L288] status=201 [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L287]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L285]
  └─ delete DataverseEntityFailureLog [L287]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L285]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L279]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L277]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L285]
      └─ ... (no implementation details available)

