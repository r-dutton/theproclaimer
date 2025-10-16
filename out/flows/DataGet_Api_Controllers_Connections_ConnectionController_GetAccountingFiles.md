[web] GET /api/connection/{apiType}/accounting-files  (DataGet.Api.Controllers.Connections.ConnectionController.GetAccountingFiles)  [L100–L108] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetAccountingFilesQuery -> GetAccountingFilesQueryHandler [L104]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountingFilesQueryHandler.Handle [L15–L38]
      └─ uses_client DataGetClient [L26]
        └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, method=GetAccountingFilesAsync, target=DataGet.Api) [L52]
          └─ target_service DataGet.Api
            └─ endpoint_recursion_suppressed DataGet.Api.Controllers.Connections.ConnectionController.GetAccountingFiles
      └─ uses_service DataGetClient
        └─ method GetAccountingFilesAsync [L26]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountingFilesAsync [L15-L302]
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
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/connection/{apitype}/accounting-files -> DataGet.Api via DataGetClient [query] (x1)
    └─ clients 1
      └─ DataGetClient
    └─ requests 1
      └─ GetAccountingFilesQuery
    └─ handlers 1
      └─ GetAccountingFilesQueryHandler

