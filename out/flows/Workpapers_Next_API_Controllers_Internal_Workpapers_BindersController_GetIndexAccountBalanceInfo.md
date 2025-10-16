[web] GET /api/internal/workpapers/binders/{binderId:Guid}/trial-balance  (Workpapers.Next.API.Controllers.Internal.Workpapers.BindersController.GetIndexAccountBalanceInfo)  [L35–L39] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request GetIndexAccountBalanceInfoQuery -> GetIndexAccountBalanceInfoQueryHandler [L38]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetIndexAccountBalanceInfoQueryHandler.Handle [L36–L358]
      └─ calls SourceRepository.ReadQuery [L129]
      └─ calls SourceAccountRepository.ReadQuery [L74]
      └─ maps_to AccountBalanceInfoDto [L65]
        └─ automapper.registration WorkpapersMappingProfile (Binder->AccountBalanceInfoDto) [L457]
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L65]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ GetIndexAccountBalanceInfoQuery
    └─ handlers 1
      └─ GetIndexAccountBalanceInfoQueryHandler
    └─ mappings 1
      └─ AccountBalanceInfoDto

