[web] GET /api/ato/individual-income-tax-returns  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetIndividualIncomeTaxReturns)  [L74–L80] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetIndividualIncomeTaxReturnQuery -> GetIndividualIncomeTaxReturnQueryHandler [L77]
    └─ handled_by GovLink.Application.Queries.IITR.GetIndividualIncomeTaxReturnQueryHandler.Handle [L89–L120]
      └─ uses_client IAtoClient [L103]
      └─ uses_service IAtoClient (AddScoped)
        └─ method RequestAsync [L103]
          └─ ... (no implementation details available)
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetIndividualIncomeTaxReturnQueryHandler.Handle [L33–L73]
      └─ uses_client DataGetClient [L60]
      └─ uses_service DataGetClient
        └─ method GetIndividualIncomeTaxReturnAsync [L60]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetIndividualIncomeTaxReturnAsync [L32-L506]
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
  └─ returns IndividualIncomeTaxReturnDto [L77]
  └─ impact_summary
    └─ clients 2
      └─ DataGetClient
      └─ IAtoClient
    └─ requests 1
      └─ GetIndividualIncomeTaxReturnQuery
    └─ handlers 1
      └─ GetIndividualIncomeTaxReturnQueryHandler
    └─ mappings 1
      └─ IndividualIncomeTaxReturnDto

