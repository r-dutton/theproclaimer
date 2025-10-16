[web] GET /api/ato/activity-statements/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetActivityStatementSummary)  [L42–L48] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetActivityStatementSummaryQuery -> GetActivityStatementSummaryQueryHandler [L45]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetActivityStatementSummaryQueryHandler.Handle [L31–L64]
      └─ uses_client DataGetClient [L58]
        └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetActivityStatementSummaryAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
          └─ target_service DataGet.Api
            └─ [web] GET /api/gov-link/activity-statements/summary  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementSummary)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy]
              └─ uses_service AtoService
                └─ method GetActivityStatementSummary [L49]
                  └─ implementation GovLink.Application.Services.AtoService.GetActivityStatementSummary [L12-L145]
      └─ uses_service DataGetClient
        └─ method GetActivityStatementSummaryAsync [L58]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetActivityStatementSummaryAsync [L32-L506]
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
        └─ method ProcessAsync [L50]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/gov-link/activity-statements/summary -> DataGet.Api via DataGetClient [query] (x1)
    └─ clients 1
      └─ DataGetClient
    └─ services 1
      └─ AtoService
    └─ requests 1
      └─ GetActivityStatementSummaryQuery
    └─ handlers 1
      └─ GetActivityStatementSummaryQueryHandler

