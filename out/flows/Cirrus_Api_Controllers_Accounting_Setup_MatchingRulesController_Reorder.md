[web] PUT /api/accounting/matching-rules/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Reorder)  [L177–L195] [auth=user,admin]
  └─ calls MatchingRuleRepository.WriteQuery [L186]
  └─ calls MatchingRuleRepository.ReadQuery [L180]
  └─ queries MatchingRule [L180]
    └─ reads_from MatchingRules
  └─ writes_to MatchingRule [L186]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method ReadQuery [L180]

