[web] PUT /api/accounting/matching-rule-sets/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.Update)  [L62–L67] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls MatchingRuleSetRepository.WriteQuery [L65]
  └─ writes_to MatchingRuleSet [L65]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method WriteQuery [L65]

