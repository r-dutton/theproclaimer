[web] GET /workpapers/v1/binders/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetBinderMinimalQuery)  [L62–L69]
  └─ calls BinderRepository.ReadQuery [L65]
  └─ queries Binder [L65]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L65]

