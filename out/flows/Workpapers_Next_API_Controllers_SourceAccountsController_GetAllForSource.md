[web] GET /api/source-accounts/for-source/{sourceId}  (Workpapers.Next.API.Controllers.SourceAccountsController.GetAllForSource)  [L71–L83] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceRepository.ReadQuery [L74]
  └─ query Source [L74]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L74]
      └─ ... (no implementation details available)
  └─ sends_request GetSourceAccountsLightQuery [L81]
  └─ sends_request GetSourceAccountsQuery [L82]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsQueryHandler.Handle [L50–L99]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L78]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L92]
          └─ ... (no implementation details available)
      └─ uses_service SourceAccountsQueryProcessor
        └─ method ProcessSourceAccounts [L95]
          └─ ... (no implementation details available)

