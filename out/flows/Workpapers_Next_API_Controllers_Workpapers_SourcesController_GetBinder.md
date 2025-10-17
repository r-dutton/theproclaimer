[web] GET /api/sources  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetBinder)  [L532–L538] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L534]
  └─ query Binder [L534]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1

