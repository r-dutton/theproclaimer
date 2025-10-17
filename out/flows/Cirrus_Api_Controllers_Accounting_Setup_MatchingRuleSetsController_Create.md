[web] POST /api/accounting/matching-rule-sets/  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.Create)  [L54–L60] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls MatchingRuleSetRepository.Add [L58]
  └─ insert MatchingRuleSet [L58]
    └─ reads_from MatchingRuleSets
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ MatchingRuleSet writes=1 reads=0

