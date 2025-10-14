[web] GET /api/hmrc  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetBinder)  [L98–L103] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L100]
  └─ queries Binder [L100]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L100]

