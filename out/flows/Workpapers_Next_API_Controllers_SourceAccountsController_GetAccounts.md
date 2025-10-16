[web] GET /api/source-accounts/  (Workpapers.Next.API.Controllers.SourceAccountsController.GetAccounts)  [L60–L65] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceAccountsByIdQuery -> GetSourceAccountsByIdQueryHandler [L64]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsByIdQueryHandler.Handle [L45–L95]
      └─ calls SourceAccountRepository.ReadQuery [L85]
      └─ maps_to SourceAccountDto [L93]
      └─ uses_service SourceAccountsQueryProcessor
        └─ method ProcessSourceAccounts [L67]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ GetSourceAccountsByIdQuery
    └─ handlers 1
      └─ GetSourceAccountsByIdQueryHandler
    └─ mappings 1
      └─ SourceAccountDto

