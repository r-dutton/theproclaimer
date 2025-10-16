[web] GET /api/hmrc  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetBinder)  [L98–L103] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L100]
  └─ query Binder [L100]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L100]
      └─ ... (no implementation details available)

