[web] PUT /api/connections/reportance/trialbalance  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetTrialBalance)  [L68–L74]
  └─ sends_request GetTrialBalanceQuery [L71]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Api.External.Queries.GetTrialBalanceQueryHandler.Handle [L44–L156]
      └─ uses_service GetAccountsQuery
        └─ method Execute [L73]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L93]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L65]

