[web] DELETE /api/matching-rules/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.Delete)  [L180–L191] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls MatchingRuleRepository.Remove [L188]
  └─ calls MatchingRuleRepository.WriteQuery [L184]
  └─ delete MatchingRule [L188]
    └─ reads_from MatchingRules
  └─ write MatchingRule [L184]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method WriteQuery [L184]
      └─ ... (no implementation details available)

