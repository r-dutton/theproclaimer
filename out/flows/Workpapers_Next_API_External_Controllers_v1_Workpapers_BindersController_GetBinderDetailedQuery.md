[web] GET /workpapers/v1/binders/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.BindersController.GetBinderDetailedQuery)  [L81–L88]
  └─ calls BinderRepository.ReadQuery [L84]
  └─ queries Binder [L84]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L84]

