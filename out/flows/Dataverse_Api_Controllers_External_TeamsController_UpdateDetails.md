[web] PUT /  (Dataverse.Api.Controllers.External.TeamsController.UpdateDetails)  [L61–L65]
  └─ sends_request CreateOrUpdateTeamCommand [L64]
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

