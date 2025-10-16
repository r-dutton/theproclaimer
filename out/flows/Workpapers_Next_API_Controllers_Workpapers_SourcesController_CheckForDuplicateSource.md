[web] GET /api/sources  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.CheckForDuplicateSource)  [L540–L560] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceRepository.ReadQuery [L548]
  └─ query Source [L548]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L548]
      └─ ... (no implementation details available)

