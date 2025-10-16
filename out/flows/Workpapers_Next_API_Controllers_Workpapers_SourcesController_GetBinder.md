[web] GET /api/sources  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetBinder)  [L532–L538] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L534]
  └─ query Binder [L534]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L534]
      └─ ... (no implementation details available)

