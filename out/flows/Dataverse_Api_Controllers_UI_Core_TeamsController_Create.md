[web] POST /api/ui/teams/  (Dataverse.Api.Controllers.UI.Core.TeamsController.Create)  [L110–L117] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamRepository.Add [L115]
  └─ writes_to Team [L115]
    └─ reads_from Teams
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L113]
  └─ uses_service IControlledRepository<Team>
    └─ method Add [L115]

