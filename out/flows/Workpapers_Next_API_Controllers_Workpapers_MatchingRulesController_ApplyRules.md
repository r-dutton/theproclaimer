[web] POST /api/matching-rules/source/{sourceId}/apply-all  (Workpapers.Next.API.Controllers.Workpapers.MatchingRulesController.ApplyRules)  [L86–L93] [auth=AuthorizationPolicies.User]
  └─ sends_request ApplyMatchingRulesCommand [L90]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ApplyMatchingRulesCommandHandler.Handle [L96–L646]
      └─ uses_service IAccountTypeProvider
        └─ method GetAccountTypesWithJurisdictionAsync [L238]
      └─ uses_service IBenchmarkCodeSetProvider
        └─ method GetBenchmarkCodeSetAsync [L225]
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L190]
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L173]
      └─ uses_service IMatchingRuleProvider
        └─ method GetRulesForFileAsync [L146]

