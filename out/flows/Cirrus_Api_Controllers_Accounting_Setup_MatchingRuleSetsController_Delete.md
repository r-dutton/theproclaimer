[web] DELETE /api/accounting/matching-rule-sets/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.Delete)  [L72–L77] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls MatchingRuleSetRepository.Remove [L76]
  └─ calls MatchingRuleSetRepository.WriteQuery [L75]
  └─ delete MatchingRuleSet [L76]
    └─ reads_from MatchingRuleSets
  └─ write MatchingRuleSet [L75]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method WriteQuery [L75]
      └─ ... (no implementation details available)

