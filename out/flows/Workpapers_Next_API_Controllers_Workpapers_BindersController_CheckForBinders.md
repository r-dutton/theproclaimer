[web] GET /api/binders/exist-for-firm  (Workpapers.Next.API.Controllers.Workpapers.BindersController.CheckForBinders)  [L127–L135] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L131]
  └─ query Binder [L131]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1

