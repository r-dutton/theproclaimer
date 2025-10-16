[web] GET /api/matching-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Get)  [L54–L63] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to MatchingRuleDto [L58]
    └─ automapper.registration CirrusMappingProfile (MatchingRule->MatchingRuleDto) [L230]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRule->MatchingRuleDto) [L887]
  └─ calls MatchingRuleRepository.ReadQuery [L58]
  └─ query MatchingRule [L58]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method ReadQuery [L58]
      └─ ... (no implementation details available)

