[web] DELETE /api/ui/teams/{id:Guid}  (Dataverse.Api.Controllers.UI.Core.TeamsController.Delete)  [L132–L137] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamRepository.WriteQuery [L135]
  └─ writes_to Team [L135]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method WriteQuery [L135]

