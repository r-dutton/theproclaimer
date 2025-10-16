[web] GET /api/matching-rules/rule-set/{ruleSetId:guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.GetAllFromSet)  [L72–L81] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to MatchingRuleDto [L76]
    └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRule->MatchingRuleDto) [L887]
  └─ calls MatchingRuleRepository.ReadQuery [L76]
  └─ query MatchingRule [L76]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method ReadQuery [L76]
      └─ ... (no implementation details available)

