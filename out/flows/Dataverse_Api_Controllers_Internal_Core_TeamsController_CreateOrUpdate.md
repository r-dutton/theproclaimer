[web] POST /api/internal/teams/create-or-update  (Dataverse.Api.Controllers.Internal.Core.TeamsController.CreateOrUpdate)  [L56–L63] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateTeamCommand [L59]
    └─ handled_by Dataverse.ApplicationService.Commands.Firms.CreateOrUpdateTeamCommandHandler.Handle [L38–L78]
      └─ maps_to TeamDto [L76]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L53]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service IControlledRepository<Team>
        └─ method WriteQuery [L60]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L76]
          └─ ... (no implementation details available)

