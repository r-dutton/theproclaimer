[web] GET /api/account-types/search  (Workpapers.Next.API.Controllers.AccountTypesController.Search)  [L59–L65] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request FindAccountTypesQuery [L64]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.FindAccountTypesQueryHandler.Handle [L41–L80]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<AccountType>
        └─ method ReadQuery [L56]
          └─ ... (no implementation details available)
  └─ sends_request FindAccountTypesLightQuery [L63]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.FindAccountTypesQueryHandler.Handle [L50–L82]
      └─ uses_service IControlledRepository<AccountType>
        └─ method ReadQuery [L78]
          └─ ... (no implementation details available)

