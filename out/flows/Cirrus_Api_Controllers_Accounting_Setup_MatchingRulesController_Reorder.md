[web] PUT /api/accounting/matching-rules/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Reorder)  [L177–L195] status=200 [auth=user,admin]
  └─ calls MatchingRuleRepository (methods: WriteQuery,ReadQuery) [L186]
  └─ write MatchingRule [L186]
    └─ reads_from MatchingRules
  └─ query MatchingRule [L180]
    └─ reads_from MatchingRules
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ MatchingRule writes=1 reads=1

