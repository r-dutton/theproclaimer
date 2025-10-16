[web] GET /api/accounting/matching-rules/rule-set/{ruleSetId}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.GetAllMasters)  [L87–L99] status=200 [auth=user,admin]
  └─ maps_to MatchingRuleDto [L90]
    └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRule->MatchingRuleDto) [L887]
  └─ calls MatchingRuleRepository.ReadQuery [L90]
  └─ query MatchingRule [L90]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method ReadQuery [L90]
      └─ ... (no implementation details available)

