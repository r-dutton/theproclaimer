[web] DELETE /api/ui/teams/{id:Guid}  (Dataverse.Api.Controllers.UI.Core.TeamsController.Delete)  [L132–L137] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamRepository.WriteQuery [L135]
  └─ write Team [L135]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method WriteQuery [L135]
      └─ ... (no implementation details available)

