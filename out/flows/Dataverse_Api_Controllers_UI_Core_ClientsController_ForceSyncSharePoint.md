[web] POST /api/ui/clients/{clientId:Guid}/share-point-sync  (Dataverse.Api.Controllers.UI.Core.ClientsController.ForceSyncSharePoint)  [L343–L357] [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L356]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L354]
  └─ writes_to DataverseEntityFailureLog [L356]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L354]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L354]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L348]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L346]

