[web] GET /api/matching-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Get)  [L54–L63] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to MatchingRuleDto [L58]
    └─ automapper.registration WorkpapersMappingProfile (MatchingRule->MatchingRuleDto) [L887]
  └─ calls MatchingRuleRepository.ReadQuery [L58]
  └─ query MatchingRule [L58]
    └─ reads_from MatchingRules
  └─ uses_service MatchingRuleRepository
    └─ method ReadQuery [L58]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleRepository.ReadQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ MatchingRule writes=0 reads=1
    └─ services 1
      └─ MatchingRuleRepository
    └─ mappings 1
      └─ MatchingRuleDto

