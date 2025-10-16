[web] PUT /api/teams/{id}  (Cirrus.Api.Controllers.Firm.TeamsController.Update)  [L79–L87] status=200 [auth=user,user]
  └─ calls TeamRepository.WriteQuery [L84]
  └─ write Team [L84]
    └─ reads_from Teams
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Team writes=1 reads=0

