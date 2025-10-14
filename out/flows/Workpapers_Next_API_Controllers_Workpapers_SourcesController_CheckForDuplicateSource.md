[web] GET /api/sources  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.CheckForDuplicateSource)  [L540–L560] [auth=AuthorizationPolicies.User]
  └─ calls SourceRepository.ReadQuery [L548]
  └─ queries Source [L548]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L548]

