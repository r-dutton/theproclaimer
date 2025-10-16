[web] POST /api/accounting/matching-rule-sets/  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.Create)  [L54–L60] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls MatchingRuleSetRepository.Add [L58]
  └─ insert MatchingRuleSet [L58]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method Add [L58]
      └─ ... (no implementation details available)

