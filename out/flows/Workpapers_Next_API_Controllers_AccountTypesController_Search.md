[web] GET /api/account-types/search  (Workpapers.Next.API.Controllers.AccountTypesController.Search)  [L59–L65] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request FindAccountTypesQuery -> FindAccountTypesQueryHandler [L64]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.FindAccountTypesQueryHandler.Handle [L41–L80]
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L62]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
      └─ uses_service IControlledRepository<AccountType> (Scoped (inferred))
        └─ method ReadQuery [L56]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountTypeRepository.ReadQuery
  └─ sends_request FindAccountTypesLightQuery -> FindAccountTypesQueryHandler [L63]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.FindAccountTypesQueryHandler.Handle [L50–L82]
      └─ calls AccountTypeRepository.ReadQuery [L78]
  └─ impact_summary
    └─ requests 2
      └─ FindAccountTypesLightQuery
      └─ FindAccountTypesQuery
    └─ handlers 1
      └─ FindAccountTypesQueryHandler

