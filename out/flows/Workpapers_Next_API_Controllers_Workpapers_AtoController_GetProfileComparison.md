[web] GET /api/ato/profile-compare  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetProfileComparison)  [L110–L116] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetIndividualIncomeTaxReturnProfileCompareQuery -> GetIndividualIncomeTaxReturnProfileCompareQueryHandler [L113]
    └─ handled_by GovLink.Application.Queries.IITR.GetIndividualIncomeTaxReturnProfileCompareQueryHandler.Handle [L116–L134]
      └─ uses_client IAtoClient [L130]
      └─ uses_service IAtoClient (AddScoped)
        └─ method RequestAsync [L130]
          └─ ... (no implementation details available)
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetIndividualIncomeTaxReturnProfileCompareQueryHandler.Handle [L33–L73]
      └─ uses_client DataGetClient [L60]
        └─ calls GetProfileComparison (GET /api/gov-link/individual-income-tax-returns/profile-compare?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetProfileCompareAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&targetFinancialYear={*}&taxAgentABN={*}&taxAgentNumber={*}) [L325]
          └─ target_service DataGet.Api
            └─ [web] GET /api/gov-link/individual-income-tax-returns/profile-compare  (DataGet.Api.Controllers.GovLink.IndividualIncomeTaxReturnController.GetProfileComparison)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy]
              └─ uses_service AtoService
                └─ method GetIncomeTaxProfileComparison [L49]
                  └─ implementation GovLink.Application.Services.AtoService.GetIncomeTaxProfileComparison [L12-L145]
      └─ uses_service DataGetClient
        └─ method GetProfileCompareAsync [L60]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetProfileCompareAsync [L32-L506]
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
  └─ returns ProfileCompareDto [L113]
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/gov-link/individual-income-tax-returns/profile-compare -> DataGet.Api via DataGetClient [query] (x1)
    └─ clients 2
      └─ DataGetClient
      └─ IAtoClient
    └─ services 1
      └─ AtoService
    └─ requests 1
      └─ GetIndividualIncomeTaxReturnProfileCompareQuery
    └─ handlers 1
      └─ GetIndividualIncomeTaxReturnProfileCompareQueryHandler
    └─ mappings 1
      └─ ProfileCompareDto

