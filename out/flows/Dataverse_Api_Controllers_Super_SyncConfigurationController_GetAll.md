[web] GET /api/super/sync-configuration/  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetAll)  [L65–L69] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetSyncConfigurationsQuery [L68]
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

