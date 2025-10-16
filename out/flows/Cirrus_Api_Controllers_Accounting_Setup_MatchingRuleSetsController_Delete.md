[web] DELETE /api/accounting/matching-rule-sets/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.Delete)  [L72–L77] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls MatchingRuleSetRepository (methods: Remove,WriteQuery) [L76]
  └─ delete MatchingRuleSet [L76]
    └─ reads_from MatchingRuleSets
  └─ write MatchingRuleSet [L75]
    └─ reads_from MatchingRuleSets
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ MatchingRuleSet writes=2 reads=0

