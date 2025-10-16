[web] POST /api/matching-rule-sets/default/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.MatchingRuleSetsController.CreateFromDefault)  [L68–L74] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request CreateRuleSetFromMasterCommand [L71]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.MatchingRuleSets.CreateRuleSetFromMasterCommandHandler.Handle [L26–L124]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L68]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
      └─ uses_service IControlledRepository<Firm>
        └─ method ReadQuery [L69]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<MatchingRule>
        └─ method ReadQuery [L73]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<MatchingRuleSet>
        └─ method ReadQuery [L50]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L98]
          └─ ... (no implementation details available)

