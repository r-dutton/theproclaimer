[web] GET /api/account-types/search  (Workpapers.Next.API.Controllers.AccountTypesController.Search)  [L59–L65] [auth=AuthorizationPolicies.User]
  └─ sends_request FindAccountTypesQuery [L64]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.FindAccountTypesQueryHandler.Handle [L41–L80]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L62]
      └─ uses_service IControlledRepository<AccountType>
        └─ method ReadQuery [L56]
  └─ sends_request FindAccountTypesLightQuery [L63]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.FindAccountTypesQueryHandler.Handle [L50–L82]
      └─ uses_service IControlledRepository<AccountType>
        └─ method ReadQuery [L78]

