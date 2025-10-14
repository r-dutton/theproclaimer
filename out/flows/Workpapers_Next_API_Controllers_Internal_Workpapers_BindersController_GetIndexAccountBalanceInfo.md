[web] GET /api/internal/workpapers/binders/{binderId:Guid}/trial-balance  (Workpapers.Next.API.Controllers.Internal.Workpapers.BindersController.GetIndexAccountBalanceInfo)  [L35–L39] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request GetIndexAccountBalanceInfoQuery [L38]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetIndexAccountBalanceInfoQueryHandler.Handle [L36–L358]
      └─ maps_to AccountBalanceInfoDto [L65]
        └─ automapper.registration WorkpapersMappingProfile (Binder->AccountBalanceInfoDto) [L457]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L65]
      └─ uses_service IControlledRepository<Source>
        └─ method ReadQuery [L129]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L74]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]

