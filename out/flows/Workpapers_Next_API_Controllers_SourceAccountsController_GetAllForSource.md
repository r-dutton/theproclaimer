[web] GET /api/source-accounts/for-source/{sourceId}  (Workpapers.Next.API.Controllers.SourceAccountsController.GetAllForSource)  [L71–L83] [auth=AuthorizationPolicies.User]
  └─ calls SourceRepository.ReadQuery [L74]
  └─ queries Source [L74]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L74]
  └─ sends_request GetSourceAccountsLightQuery [L81]
  └─ sends_request GetSourceAccountsQuery [L82]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsQueryHandler.Handle [L50–L99]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L78]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L92]
      └─ uses_service SourceAccountsQueryProcessor
        └─ method ProcessSourceAccounts [L95]

