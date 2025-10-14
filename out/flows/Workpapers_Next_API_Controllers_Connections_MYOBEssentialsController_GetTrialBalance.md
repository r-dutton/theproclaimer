[web] PUT /api/connections/myob/es/trialbalance  (Workpapers.Next.API.Controllers.Connections.MYOBEssentialsController.GetTrialBalance)  [L42–L48]
  └─ uses_service UserService
    └─ method GetUserId [L45]
  └─ sends_request GetTrialBalanceQuery [L45]
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

