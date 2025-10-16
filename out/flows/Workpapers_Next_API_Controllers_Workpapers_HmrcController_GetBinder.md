[web] GET /api/hmrc  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetBinder)  [L98–L103] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L100]
  └─ query Binder [L100]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1

