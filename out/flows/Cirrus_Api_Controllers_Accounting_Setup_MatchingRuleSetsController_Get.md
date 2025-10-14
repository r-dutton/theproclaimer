[web] GET /api/accounting/matching-rule-sets/{id}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.Get)  [L34–L41] [auth=Authentication.UserPolicy]
  └─ maps_to MatchingRuleSetDto [L37]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L889]
    └─ automapper.registration CirrusMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L229]
  └─ calls MatchingRuleSetRepository.ReadQuery [L37]
  └─ queries MatchingRuleSet [L37]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method ReadQuery [L37]

