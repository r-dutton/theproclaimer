[web] DELETE /api/ui/teams/{id:Guid}  (Dataverse.Api.Controllers.UI.Core.TeamsController.Delete)  [L132–L137] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamRepository.WriteQuery [L135]
  └─ write Team [L135]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method WriteQuery [L135]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.WriteQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Team writes=1 reads=0
    └─ services 1
      └─ TeamRepository

