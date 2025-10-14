[web] GET /api/source-accounts/  (Workpapers.Next.API.Controllers.SourceAccountsController.GetAccounts)  [L60–L65] [auth=AuthorizationPolicies.User]
  └─ sends_request GetSourceAccountsByIdQuery [L64]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsByIdQueryHandler.Handle [L45–L95]
      └─ maps_to SourceAccountDto [L93]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L85]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L93]
      └─ uses_service SourceAccountsQueryProcessor
        └─ method ProcessSourceAccounts [L67]

