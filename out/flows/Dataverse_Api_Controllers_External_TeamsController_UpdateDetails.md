[web] PUT /  (Dataverse.Api.Controllers.External.TeamsController.UpdateDetails)  [L61–L65] status=200
  └─ sends_request CreateOrUpdateTeamCommand [L64]
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

