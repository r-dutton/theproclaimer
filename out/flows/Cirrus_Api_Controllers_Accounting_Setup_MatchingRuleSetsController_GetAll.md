[web] GET /api/accounting/matching-rule-sets/  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRuleSetsController.GetAll)  [L43–L52] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to MatchingRuleSetDto [L46]
    └─ automapper.registration CirrusMappingProfile (MatchingRuleSet->MatchingRuleSetDto) [L229]
  └─ calls MatchingRuleSetRepository.ReadQuery [L46]
  └─ query MatchingRuleSet [L46]
    └─ reads_from MatchingRuleSets
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatchingRuleSet writes=0 reads=1
    └─ mappings 1
      └─ MatchingRuleSetDto

