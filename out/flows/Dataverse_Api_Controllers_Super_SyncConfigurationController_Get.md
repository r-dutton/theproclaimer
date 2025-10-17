[web] GET /api/super/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Get)  [L50–L54] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetSyncConfigurationQuery -> GetSyncConfigurationQueryHandler [L53]
    └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.GetSyncConfigurationQueryHandler.Handle [L34–L90]
      └─ maps_to SyncConfigurationDto [L54]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<SyncConfiguration> (Scoped (inferred))
        └─ method ReadQuery [L49]
          └─ implementation Dataverse.Data.Repository.Sync.SyncConfigurationRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetSyncConfigurationQuery
    └─ handlers 1
      └─ GetSyncConfigurationQueryHandler
    └─ mappings 1
      └─ SyncConfigurationDto

