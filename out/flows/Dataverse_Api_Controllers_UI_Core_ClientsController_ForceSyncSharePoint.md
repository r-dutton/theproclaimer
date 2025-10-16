[web] POST /api/ui/clients/{clientId:Guid}/share-point-sync  (Dataverse.Api.Controllers.UI.Core.ClientsController.ForceSyncSharePoint)  [L343–L357] status=201 [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L356]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L354]
  └─ delete DataverseEntityFailureLog [L356]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L354]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L348]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L346]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L354]
      └─ ... (no implementation details available)

