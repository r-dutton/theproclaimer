[web] GET /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts)  [L44–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetAccountsQuery -> GetAccountsQueryHandler [L48]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
      └─ uses_client DataGetClient [L40]
        └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
          └─ target_service DataGet.Api
            └─ endpoint_recursion_suppressed DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts
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
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/accounting-file/{fileid}/accounts -> DataGet.Api via DataGetClient [query] (x1)
    └─ clients 1
      └─ DataGetClient
    └─ requests 1
      └─ GetAccountsQuery
    └─ handlers 1
      └─ GetAccountsQueryHandler

