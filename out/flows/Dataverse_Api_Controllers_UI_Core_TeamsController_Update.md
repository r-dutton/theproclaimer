[web] PUT /api/ui/teams/{id}  (Dataverse.Api.Controllers.UI.Core.TeamsController.Update)  [L122–L127] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request UpdateTeamWithUsersCommand [L125]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Teams.UpdateTeamWithUsersCommandHandler.Handle [L30–L127]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L51]
      └─ uses_service IControlledRepository<Team>
        └─ method WriteQuery [L52]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L65]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L77]

