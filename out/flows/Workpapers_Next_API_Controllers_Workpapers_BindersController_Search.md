[web] GET /api/binders/  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Search)  [L109–L114] [auth=AuthorizationPolicies.User]
  └─ sends_request FindBindersQuery [L113]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.FindBindersQueryHandler.Handle [L48–L167]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L89]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L90]
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method ReadQuery [L104]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L81]
      └─ uses_service UserService
        └─ method GetUser [L89]

