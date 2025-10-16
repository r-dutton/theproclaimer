[web] GET /api/binders/exist-for-firm  (Workpapers.Next.API.Controllers.Workpapers.BindersController.CheckForBinders)  [L127–L135] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L131]
  └─ query Binder [L131]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L131]
      └─ ... (no implementation details available)

