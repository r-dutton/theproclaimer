[web] GET /api/internal/workpapers/binders/exist-for-client/{dataverseClientId:guid}  (Workpapers.Next.API.Controllers.Internal.Workpapers.BindersController.CheckForClientBinders)  [L47–L56] [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ calls BinderRepository.ReadQuery [L50]
  └─ queries Binder [L50]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L50]

