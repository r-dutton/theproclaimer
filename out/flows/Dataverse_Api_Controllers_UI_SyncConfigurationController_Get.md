[web] GET /api/ui/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.UI.SyncConfigurationController.Get)  [L59–L63] [auth=Authentication.AdminPolicy]
  └─ sends_request GetSyncConfigurationQuery [L62]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.GetSyncConfigurationQueryHandler.Handle [L34–L90]
      └─ maps_to SyncConfigurationDto [L54]
      └─ uses_service IControlledRepository<SyncConfiguration>
        └─ method ReadQuery [L49]
      └─ uses_service IMapper
        └─ method Map [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]

