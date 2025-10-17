[web] POST /api/accounting/matching-rules/  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Create)  [L144–L156] status=201 [auth=user]
  └─ calls MatchingRuleRepository (methods: Add,WriteQuery) [L154]
  └─ insert MatchingRule [L154]
    └─ reads_from MatchingRules
  └─ write MatchingRule [L148]
    └─ reads_from MatchingRules
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ MatchingRule writes=2 reads=0

