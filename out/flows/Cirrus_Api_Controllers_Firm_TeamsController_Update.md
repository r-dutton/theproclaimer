[web] PUT /api/teams/{id}  (Cirrus.Api.Controllers.Firm.TeamsController.Update)  [L79–L87] status=200 [auth=user,user]
  └─ calls TeamRepository.WriteQuery [L84]
  └─ write Team [L84]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method WriteQuery [L84]
      └─ ... (no implementation details available)

