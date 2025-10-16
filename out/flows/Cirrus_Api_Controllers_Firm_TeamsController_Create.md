[web] POST /api/teams/  (Cirrus.Api.Controllers.Firm.TeamsController.Create)  [L68–L74] status=201 [auth=user,admin]
  └─ calls TeamRepository.Add [L72]
  └─ insert Team [L72]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method Add [L72]
      └─ ... (no implementation details available)

