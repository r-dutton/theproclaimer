[web] PUT /api/ui/sync-configuration/{id:Guid}/start  (Dataverse.Api.Controllers.UI.SyncConfigurationController.StartSync)  [L167–L177] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L170]
  └─ write SyncConfiguration [L170]
    └─ reads_from SyncConfigurations
  └─ uses_service RequestRegionService
    └─ method GetTimeZone [L174]
      └─ implementation Dataverse.Services.Features.Localisation.RequestRegionService.GetTimeZone [L10-L74]
      └─ implementation Dataverse.Services.Features.Localisation.RequestRegionService.GetTimeZone [L10-L74]
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L170]
      └─ ... (no implementation details available)

