[web] PUT /api/accounting/matching-rules/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Reorder)  [L177–L195] status=200 [auth=user,admin]
  └─ calls MatchingRuleRepository.WriteQuery [L186]
  └─ calls MatchingRuleRepository.ReadQuery [L180]
  └─ query MatchingRule [L180]
    └─ reads_from MatchingRules
  └─ write MatchingRule [L186]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method ReadQuery [L180]
      └─ ... (no implementation details available)

