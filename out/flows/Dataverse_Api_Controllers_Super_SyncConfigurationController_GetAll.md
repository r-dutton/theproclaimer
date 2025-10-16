[web] GET /api/super/sync-configuration/  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetAll)  [L65–L69] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetSyncConfigurationsQuery -> GetSyncConfigurationsQueryHandler [L68]
    └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.GetSyncConfigurationsQueryHandler.Handle [L32–L97]
      └─ maps_to SyncConfigurationLightDto [L60]
      └─ maps_to SyncConfigurationDto [L56]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L69]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<SyncConfiguration> (Scoped (inferred))
        └─ method ReadQuery [L49]
          └─ implementation Dataverse.Data.Repository.Sync.SyncConfigurationRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetSyncConfigurationsQuery
    └─ handlers 1
      └─ GetSyncConfigurationsQueryHandler
    └─ mappings 2
      └─ SyncConfigurationDto
      └─ SyncConfigurationLightDto

