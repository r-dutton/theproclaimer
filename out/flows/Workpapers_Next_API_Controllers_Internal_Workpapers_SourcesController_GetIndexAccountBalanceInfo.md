[web] PUT /api/internal/workpapers/sources/account-change-notification/{fileId:guid}  (Workpapers.Next.API.Controllers.Internal.Workpapers.SourcesController.GetIndexAccountBalanceInfo)  [L29–L42] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ calls SourceRepository.WriteQuery [L34]
  └─ writes_to Source [L34]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L34]

