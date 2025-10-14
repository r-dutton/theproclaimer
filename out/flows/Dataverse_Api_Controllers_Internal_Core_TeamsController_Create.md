[web] POST /api/internal/teams/  (Dataverse.Api.Controllers.Internal.Core.TeamsController.Create)  [L68–L74] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateTeamCommand [L72]
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

