[web] PUT /api/connections/reportance/trialbalance  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetTrialBalance)  [L68–L74] status=200
  └─ sends_request GetTrialBalanceQuery -> GetTrialBalanceQueryHandler [L71]
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
    └─ requests 3
      └─ GetAccountsQuery
      └─ GetTrialBalanceForDatesQuery
      └─ GetTrialBalanceQuery
    └─ handlers 3
      └─ GetAccountsQueryHandler
      └─ GetTrialBalanceForDatesQueryHandler
      └─ GetTrialBalanceQueryHandler

