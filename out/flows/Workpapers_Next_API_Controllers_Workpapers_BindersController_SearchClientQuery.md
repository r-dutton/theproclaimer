[web] GET /api/binders/client-queries  (Workpapers.Next.API.Controllers.Workpapers.BindersController.SearchClientQuery)  [L116–L121] [auth=AuthorizationPolicies.User]
  └─ sends_request FindClientQueriesForBindersQuery [L120]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.FindClientQueryHandler.Handle [L40–L119]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L85]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L84]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L70]
      └─ uses_service UserService
        └─ method GetUser [L85]

