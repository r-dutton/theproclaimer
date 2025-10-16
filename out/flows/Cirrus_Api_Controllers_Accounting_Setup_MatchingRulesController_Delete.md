[web] DELETE /api/accounting/matching-rules/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Delete)  [L200–L208] status=200 [auth=user]
  └─ calls MatchingRuleRepository (methods: Remove,WriteQuery) [L205]
  └─ delete MatchingRule [L205]
    └─ reads_from MatchingRules
  └─ write MatchingRule [L203]
    └─ reads_from MatchingRules
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ MatchingRule writes=2 reads=0

