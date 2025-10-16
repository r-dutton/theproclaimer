[web] GET /api/accounting/matching-rule-sets/  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.GetAll)  [L43–L52] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to MatchingRuleSetDto [L46]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L889]
    └─ automapper.registration CirrusMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L229]
  └─ calls MatchingRuleSetRepository.ReadQuery [L46]
  └─ query MatchingRuleSet [L46]
    └─ reads_from MatchingRuleSets
  └─ uses_service IControlledRepository<MatchingRuleSet>
    └─ method ReadQuery [L46]
      └─ ... (no implementation details available)

