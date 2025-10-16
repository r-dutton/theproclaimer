[web] DELETE /api/matching-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Delete)  [L180–L191] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleRepository (methods: Remove,WriteQuery) [L188]
  └─ delete MatchingRule [L188]
    └─ reads_from MatchingRules
  └─ write MatchingRule [L184]
    └─ reads_from MatchingRules
  └─ uses_service MatchingRuleRepository
    └─ method WriteQuery [L184]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatchingRuleRepository.WriteQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ MatchingRule writes=2 reads=0
    └─ services 1
      └─ MatchingRuleRepository

