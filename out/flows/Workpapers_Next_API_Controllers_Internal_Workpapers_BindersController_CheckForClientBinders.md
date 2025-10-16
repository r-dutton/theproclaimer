[web] GET /api/internal/workpapers/binders/exist-for-client/{dataverseClientId:guid}  (Workpapers.Next.API.Controllers.Internal.Workpapers.BindersController.CheckForClientBinders)  [L47–L56] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ calls BinderRepository.ReadQuery [L50]
  └─ query Binder [L50]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1

