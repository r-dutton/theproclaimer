[web] PUT /api/reportsettings/policies/  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.SavePolicyBindingModel)  [L128–L139] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L133]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request GetEditablePolicyQuery -> GetEditablePolicyQueryHandler [L133]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetEditablePolicyQueryHandler.Handle [L13–L48]
      └─ uses_service RequestProcessor
        └─ method Process [L39]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.Process
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.Process
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.Process
          └─ +1 additional_requests elided
      └─ uses_service UnitOfWork
        └─ method Table [L27]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ GetEditablePolicyQuery
    └─ handlers 1
      └─ GetEditablePolicyQueryHandler
    └─ caches 1
      └─ IMemoryCache

