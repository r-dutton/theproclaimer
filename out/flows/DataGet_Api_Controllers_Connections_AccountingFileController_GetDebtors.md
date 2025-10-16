[web] GET /api/accounting-file/{fileId}/debtors  (DataGet.Api.Controllers.Connections.AccountingFileController.GetDebtors)  [L119–L127] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDebtorsQuery -> GetDebtorsQueryHandler [L123]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetDebtorsQueryHandler.Handle [L25–L56]
      └─ uses_client DataGetClient [L52]
        └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, method=GetDebtorsAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
          └─ target_service DataGet.Api
            └─ endpoint_recursion_suppressed DataGet.Api.Controllers.Connections.AccountingFileController.GetDebtors
      └─ uses_service DataGetClient
        └─ method GetDebtorsAsync [L52]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetDebtorsAsync [L15-L302]
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
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L45]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L40]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
            └─ uses_service IControlledRepository<Source> (Scoped (inferred))
              └─ method GetFileIdFromSource [L25]
                └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.GetFileIdFromSource
  └─ returns Debtors [L123]
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/accounting-file/{fileid}/debtors -> DataGet.Api via DataGetClient [query] (x1)
    └─ clients 1
      └─ DataGetClient
    └─ requests 1
      └─ GetDebtorsQuery
    └─ handlers 1
      └─ GetDebtorsQueryHandler
    └─ mappings 1
      └─ Debtors

