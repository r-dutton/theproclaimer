[web] PUT /api/ui/sync-configuration/{id:Guid}/start  (Dataverse.Api.Controllers.UI.SyncConfigurationController.StartSync)  [L167–L177] [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L170]
  └─ writes_to SyncConfiguration [L170]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L170]
  └─ uses_service RequestRegionService
    └─ method GetTimeZone [L174]

