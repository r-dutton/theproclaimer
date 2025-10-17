[web] GET /api/ato/gov-link/activity-statements  (Workpapers.Next.API.Controllers.Workpapers.AtoController.RefreshActivityStatements)  [L118–L125] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.GovLink]
  └─ sends_request GetActivityStatementsQuery -> GetActivityStatementsQueryHandler [L122]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetActivityStatementsQueryHandler.Handle [L32–L66]
      └─ uses_client DataGetClient [L60]
        └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetActivityStatementsAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
          └─ target_service DataGet.Api
            └─ [web] GET /api/gov-link/activity-statements/detail  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementsDetail)  [L33–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
              └─ uses_service AtoService
                └─ method GetActivityStatements [L38]
                  └─ implementation GovLink.Application.Services.AtoService.GetActivityStatements [L12-L145]
      └─ uses_service DataGetClient
        └─ method GetActivityStatementsAsync [L60]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetActivityStatementsAsync [L32-L506]
            └─ calls GetAccountingFiles [L52]
            └─ calls GetAccounts [L65]
            └─ calls GetMovements [L93]
            └─ calls GetTransactions [L116]
            └─ calls PostJournal [L127]
            └─ calls DeleteJournal [L141]
            └─ calls GetCreditors [L156]
            └─ calls GetDebtors [L171]
            └─ calls GetWages [L189]
            └─ calls GetWages [L190]
            └─ calls GetWages [L191]
            └─ calls GetWages [L192]
            └─ calls GetWages [L193]
            └─ calls GetActivityStatementsDetail [L214]
            └─ calls GetActivityStatementSummary [L231]
            └─ calls GetTransactionDetail [L250]
            └─ calls GetTransactionSummary [L269]
            └─ calls GetReportSummary [L307]
            └─ calls GetProfileComparison [L325]
            └─ calls GetVatPackage [L343]
            └─ calls GetVatObligations [L358]
            └─ calls GetVatObligations [L358]
            └─ calls SubmitVatReturn [L367]
            └─ calls SubmitVatReturn [L367]
            └─ calls ValidateFraudHeaders [L377]
            └─ calls ValidateFraudHeaders [L377]
            └─ calls GetFraudHeaderFeedback [L387]
            └─ calls GetFraudHeaderFeedback [L387]
            └─ calls GetAuthorisationUrl [L449]
            └─ calls Disconnect [L459]
            └─ calls TestConnection [L472]
            └─ calls StoreExistingToken [L483]
            └─ calls StoreExistingFileTokens [L493]
            └─ calls DisconnectFile [L503]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L52]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
    └─ handled_by GovLink.Application.Queries.ActivityStatements.GetActivityStatementQueryHandler.Handle [L90–L198]
      └─ uses_client IAtoClient [L110]
      └─ uses_service IAtoClient (AddScoped)
        └─ method RequestAsync [L110]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/gov-link/activity-statements/detail -> DataGet.Api via DataGetClient [query] (x1)
    └─ clients 2
      └─ DataGetClient
      └─ IAtoClient
    └─ services 1
      └─ AtoService
    └─ requests 1
      └─ GetActivityStatementsQuery
    └─ handlers 2
      └─ GetActivityStatementQueryHandler
      └─ GetActivityStatementsQueryHandler

