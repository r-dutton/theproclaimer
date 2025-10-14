[web] POST /api/matching-rule-sets/default/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.CreateFromDefault)  [L68–L74] [auth=AuthorizationPolicies.Administrator]
  └─ sends_request CreateRuleSetFromMasterCommand [L71]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.MatchingRuleSets.CreateRuleSetFromMasterCommandHandler.Handle [L26–L124]
      └─ uses_service IControlledRepository<Firm>
        └─ method ReadQuery [L69]
      └─ uses_service IControlledRepository<MatchingRule>
        └─ method ReadQuery [L73]
      └─ uses_service IControlledRepository<MatchingRuleSet>
        └─ method ReadQuery [L50]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L98]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L68]

