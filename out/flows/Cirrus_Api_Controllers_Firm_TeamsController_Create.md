[web] POST /api/teams/  (Cirrus.Api.Controllers.Firm.TeamsController.Create)  [L68–L74] status=201 [auth=user,admin]
  └─ calls TeamRepository.Add [L72]
  └─ insert Team [L72]
    └─ reads_from Teams
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Team writes=1 reads=0

