[web] POST /api/teams/  (Cirrus.Api.Controllers.Firm.TeamsController.Create)  [L68–L74] [auth=user,admin]
  └─ calls TeamRepository.Add [L72]
  └─ writes_to Team [L72]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method Add [L72]

