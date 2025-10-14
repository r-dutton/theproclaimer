[web] DELETE /api/teams/{id:Guid}  (Cirrus.Api.Controllers.Firm.TeamsController.Delete)  [L92–L98] [auth=user,admin]
  └─ calls TeamRepository.Remove [L96]
  └─ calls TeamRepository.WriteQuery [L95]
  └─ writes_to Team [L96]
    └─ reads_from Teams
  └─ writes_to Team [L95]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method WriteQuery [L95]

