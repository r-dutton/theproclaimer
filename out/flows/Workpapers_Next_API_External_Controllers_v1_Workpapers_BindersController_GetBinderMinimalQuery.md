[web] GET /workpapers/v1/binders/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetBinderMinimalQuery)  [L62–L69] status=200
  └─ calls BinderRepository.ReadQuery [L65]
  └─ query Binder [L65]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L65]
      └─ ... (no implementation details available)

