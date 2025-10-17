[web] GET /workpapers/v1/binders/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetBinderMinimalQuery)  [L62–L69] status=200
  └─ calls BinderRepository.ReadQuery [L65]
  └─ query Binder [L65]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1

