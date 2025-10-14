[web] GET /api/sources/taxonomy  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetBinderTaxonomy)  [L433–L439] [auth=AuthorizationPolicies.User]
  └─ sends_request GetBinderTaxonomyQuery [L436]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetBinderTaxonomyQueryHandler.Handle [L32–L113]
      └─ uses_service ConnectionApiService
        └─ method GetApiMethods [L86]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L58]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L92]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L56]

