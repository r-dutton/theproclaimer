[web] PUT /api/connections/myob/es/trialbalance  (Workpapers.Next.API.Controllers.Connections.MYOBEssentialsController.GetTrialBalance)  [L42–L48] status=200
  └─ uses_service UserService
    └─ method GetUserId [L45]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request GetTrialBalanceQuery -> GetTrialBalanceQueryHandler [L45]
    └─ handled_by Cirrus.Api.External.Queries.GetTrialBalanceQueryHandler.Handle [L44–L156]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L93]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.Execute
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
      └─ uses_service GetAccountsQuery
        └─ method Execute [L73]
          └─ resolves_request Cirrus.Connections.DataGet.Queries.GetAccountsQuery.Execute
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
          └─ resolves_request DataGet.Connections.App.Queries.GetAccountsQuery.Execute
          └─ resolves_request DataGet.Connections.External.Bgl360.Api.Queries.GetAccountsQuery.Execute
          └─ +4 additional_requests elided
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L65]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 3
      └─ GetAccountsQuery
      └─ GetTrialBalanceForDatesQuery
      └─ GetTrialBalanceQuery
    └─ handlers 3
      └─ GetAccountsQueryHandler
      └─ GetTrialBalanceForDatesQueryHandler
      └─ GetTrialBalanceQueryHandler
    └─ caches 1
      └─ IMemoryCache

