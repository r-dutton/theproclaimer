[web] POST /api/sources/  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.Create)  [L118–L135] status=201 [auth=AuthorizationPolicies.User]
  └─ calls SourceRepository.Add [L132]
  └─ insert Source [L132]
  └─ query SourceType [L123]
  └─ uses_service IControlledRepository<Source>
    └─ method Add [L132]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<SourceType>
    └─ method ReadQuery [L123]
      └─ ... (no implementation details available)

