[web] POST /api/ui/teams/  (Dataverse.Api.Controllers.UI.Core.TeamsController.Create)  [L110–L117] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamRepository.Add [L115]
  └─ insert Team [L115]
    └─ reads_from Teams
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L113]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Team>
    └─ method Add [L115]
      └─ ... (no implementation details available)

