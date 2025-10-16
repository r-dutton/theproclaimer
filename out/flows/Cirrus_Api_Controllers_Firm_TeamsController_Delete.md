[web] DELETE /api/teams/{id:Guid}  (Cirrus.Api.Controllers.Firm.TeamsController.Delete)  [L92–L98] status=200 [auth=user,admin]
  └─ calls TeamRepository.Remove [L96]
  └─ calls TeamRepository.WriteQuery [L95]
  └─ delete Team [L96]
    └─ reads_from Teams
  └─ write Team [L95]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method WriteQuery [L95]
      └─ ... (no implementation details available)

