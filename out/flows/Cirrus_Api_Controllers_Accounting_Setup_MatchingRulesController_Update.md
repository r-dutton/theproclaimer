[web] PUT /api/accounting/matching-rules/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Setup.MatchingRulesController.Update)  [L161–L168] status=200 [auth=user]
  └─ calls MatchingRuleRepository.WriteQuery [L164]
  └─ write MatchingRule [L164]
    └─ reads_from MatchingRules
  └─ uses_service IControlledRepository<MatchingRule>
    └─ method WriteQuery [L164]
      └─ ... (no implementation details available)

