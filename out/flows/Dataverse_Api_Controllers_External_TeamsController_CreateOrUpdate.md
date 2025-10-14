[web] POST /create-or-update  (Dataverse.Api.Controllers.External.TeamsController.CreateOrUpdate)  [L52–L59]
  └─ sends_request CreateOrUpdateTeamCommand [L55]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Firms.CreateOrUpdateTeamCommandHandler.Handle [L38–L78]
      └─ maps_to TeamDto [L76]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L53]
      └─ uses_service IControlledRepository<Team>
        └─ method WriteQuery [L60]
      └─ uses_service IMapper
        └─ method Map [L76]

