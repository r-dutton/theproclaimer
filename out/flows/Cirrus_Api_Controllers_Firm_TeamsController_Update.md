[web] PUT /api/teams/{id}  (Cirrus.Api.Controllers.Firm.TeamsController.Update)  [L79–L87] [auth=user,user]
  └─ calls TeamRepository.WriteQuery [L84]
  └─ writes_to Team [L84]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method WriteQuery [L84]

