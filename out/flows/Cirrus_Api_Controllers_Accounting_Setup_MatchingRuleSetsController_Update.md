[web] PUT /api/accounting/matching-rule-sets/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.Update)  [L62–L67] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls MatchingRuleSetRepository.WriteQuery [L65]
  └─ write MatchingRuleSet [L65]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method WriteQuery [L65]
      └─ ... (no implementation details available)

