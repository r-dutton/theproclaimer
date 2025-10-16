[web] GET /workpapers/v1/binders/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetBinderDetailedQuery)  [L81–L88] status=200
  └─ calls BinderRepository.ReadQuery [L84]
  └─ query Binder [L84]
    └─ reads_from Binders
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1

