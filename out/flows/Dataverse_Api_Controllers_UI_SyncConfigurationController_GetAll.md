[web] GET /api/ui/sync-configuration/  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetAll)  [L53–L57] [auth=Authentication.AdminPolicy]
  └─ sends_request GetSyncConfigurationsQuery [L56]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.GetSyncConfigurationsQueryHandler.Handle [L32–L97]
      └─ maps_to SyncConfigurationDto [L56]
      └─ maps_to SyncConfigurationLightDto [L60]
      └─ uses_service IControlledRepository<SyncConfiguration>
        └─ method ReadQuery [L49]
      └─ uses_service IMapper
        └─ method Map [L56]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L69]

