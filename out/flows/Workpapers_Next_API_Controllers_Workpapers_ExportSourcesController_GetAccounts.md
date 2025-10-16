[web] GET /api/sources/export/{id:guid}/accounts  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetAccounts)  [L68–L73] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetExportSourceAccountsQuery -> GetExportSourceAccountsQueryHandler [L71]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourceAccountsQueryHandler.Handle [L29–L68]
      └─ uses_client DataGetClient [L46]
        └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
          └─ target_service DataGet.Api
            └─ [web] GET /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts)  [L44–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
              └─ sends_request GetAccountsQuery -> GetAccountsQueryHandler [L48]
                └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
                  └─ uses_client DataGetClient [L40]
                    └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                      └─ remote_endpoint_expansion_suppressed (see previous expansion)
                  └─ uses_service DataGetClient
                    └─ method GetAccountsAsync [L40]
                      └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountsAsync [L15-L302]
                        └─ calls GetAuthorisationUrl [L45]
                        └─ calls Disconnect [L55]
                        └─ calls DisconnectFile [L65]
                        └─ calls GetAccountingFiles [L74]
                        └─ calls TestConnection [L84]
                        └─ calls GetSourceDivisions [L95]
                        └─ calls GetAccounts [L106]
                        └─ calls GetMovements [L130]
                        └─ calls GetTransactions [L151]
                        └─ calls PostJournal [L161]
                        └─ calls PostAccount [L171]
                        └─ calls DeleteJournal [L182]
                        └─ calls GetCreditors [L194]
                        └─ calls GetDebtors [L206]
                        └─ calls GetWages [L218]
                        └─ calls StoreExistingToken [L228]
                        └─ calls StoreExistingFileTokens [L238]
                        └─ calls RequestLodgementAsync [L244]
                  └─ uses_service DatagetFileIdService
                    └─ method GetFileIdFromSource [L38]
                      └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
                        └─ uses_service IControlledRepository<Source> (Scoped (inferred))
                          └─ method GetFileIdFromSource [L25]
                            └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.GetFileIdFromSource
      └─ uses_service DataGetClient
        └─ method GetAccountsAsync [L46]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetAccountsAsync [L32-L506]
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
      └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
        └─ method ReadQuery [L42]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.ReadQuery
  └─ impact_summary
    └─ remote_endpoints 1 (calls=2) scopes=service:2
      └─ GET /api/accounting-file/{fileid}/accounts -> DataGet.Api via DataGetClient [query] (x2)
    └─ clients 1
      └─ DataGetClient
    └─ requests 2
      └─ GetAccountsQuery
      └─ GetExportSourceAccountsQuery
    └─ handlers 2
      └─ GetAccountsQueryHandler
      └─ GetExportSourceAccountsQueryHandler

