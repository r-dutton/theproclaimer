[web] GET /api/matching-rules/rule-set/{ruleSetId:guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.GetAllFromSet)  [L72–L81] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to MatchingRuleDto [L76]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRule->MatchingRuleDto) [L887]
  └─ calls MatchingRuleRepository.ReadQuery [L76]
  └─ query MatchingRule [L76]
    └─ reads_from MatchingRules
  └─ uses_service MatchingRuleRepository
    └─ method ReadQuery [L76]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleRepository.ReadQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatchingRule writes=0 reads=1
    └─ services 1
      └─ MatchingRuleRepository
    └─ mappings 1
      └─ MatchingRuleDto

