[web] GET /api/source-accounts/for-source/{sourceId}  (Workpapers.Next.API.Controllers.SourceAccountsController.GetAllForSource)  [L71–L83] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceRepository.ReadQuery [L74]
  └─ query Source [L74]
  └─ uses_service SourceRepository
    └─ method ReadQuery [L74]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceRepository.ReadQuery [L8-L40]
  └─ sends_request GetSourceAccountsQuery -> GetSourceAccountsQueryHandler [L82]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsQueryHandler.Handle [L50–L99]
      └─ calls SourceAccountRepository.ReadQuery [L78]
      └─ uses_service SourceAccountsQueryProcessor
        └─ method ProcessSourceAccounts [L95]
          └─ ... (no implementation details available)
  └─ sends_request GetSourceAccountsLightQuery [L81]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Source writes=0 reads=1
    └─ services 1
      └─ SourceRepository
    └─ requests 2
      └─ GetSourceAccountsLightQuery
      └─ GetSourceAccountsQuery
    └─ handlers 1
      └─ GetSourceAccountsQueryHandler

