[web] GET /api/sources/{type}/journal-url  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetJournalUrl)  [L455–L476] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L458]
  └─ queries Binder [L458]
    └─ reads_from Binders
  └─ uses_service IConnectionApiService (AddSingleton)
    └─ method GetApiMethods [L472]
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L458]

