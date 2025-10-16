[web] DELETE /api/teams/{id:Guid}  (Cirrus.Api.Controllers.Firm.TeamsController.Delete)  [L92–L98] status=200 [auth=user,admin]
  └─ calls TeamRepository (methods: Remove,WriteQuery) [L96]
  └─ delete Team [L96]
    └─ reads_from Teams
  └─ write Team [L95]
    └─ reads_from Teams
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ Team writes=2 reads=0

