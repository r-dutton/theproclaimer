[web] POST /api/matching-rules/source/{sourceId}/apply-all  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.ApplyRules)  [L86–L93] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request ApplyMatchingRulesCommand [L90]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ApplyMatchingRulesCommandHandler.Handle [L96–L646]
      └─ uses_service IAccountTypeProvider
        └─ method GetAccountTypesWithJurisdictionAsync [L238]
          └─ ... (no implementation details available)
      └─ uses_service IBenchmarkCodeSetProvider
        └─ method GetBenchmarkCodeSetAsync [L225]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L190]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L173]
          └─ ... (no implementation details available)
      └─ uses_service IMatchingRuleProvider
        └─ method GetRulesForFileAsync [L146]
          └─ ... (no implementation details available)

