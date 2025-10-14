[web] DELETE /api/accounting/matching-rule-sets/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.Delete)  [L72–L77] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls MatchingRuleSetRepository.Remove [L76]
  └─ calls MatchingRuleSetRepository.WriteQuery [L75]
  └─ writes_to MatchingRuleSet [L76]
    └─ reads_from MatchingRuleSets
  └─ writes_to MatchingRuleSet [L75]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method WriteQuery [L75]

