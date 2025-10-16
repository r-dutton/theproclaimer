[web] GET /api/sources/{type}/journal-url  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetJournalUrl)  [L455–L476] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L458]
  └─ query Binder [L458]
    └─ reads_from Binders
  └─ uses_service IConnectionApiService (AddSingleton)
    └─ method GetApiMethods [L472]
      └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L458]
      └─ ... (no implementation details available)

