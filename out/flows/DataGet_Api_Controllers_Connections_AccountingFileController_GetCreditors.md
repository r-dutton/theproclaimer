[web] GET /api/accounting-file/{fileId}/creditors  (DataGet.Api.Controllers.Connections.AccountingFileController.GetCreditors)  [L109–L117] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCreditorsQuery -> GetCreditorsQueryHandler [L113]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetCreditorsQueryHandler.Handle [L25–L57]
      └─ uses_client DataGetClient [L53]
        └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, method=GetCreditorsAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
          └─ target_service DataGet.Api
            └─ endpoint_recursion_suppressed DataGet.Api.Controllers.Connections.AccountingFileController.GetCreditors
      └─ uses_service DataGetClient
        └─ method GetCreditorsAsync [L53]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetCreditorsAsync [L15-L302]
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
        └─ method ReadQuery [L46]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L40]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
            └─ uses_service IControlledRepository<Source> (Scoped (inferred))
              └─ method GetFileIdFromSource [L25]
                └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.GetFileIdFromSource
  └─ returns Creditors [L113]
  └─ impact_summary
    └─ remote_endpoints 1 (calls=1) scopes=service:1
      └─ GET /api/accounting-file/{fileid}/creditors -> DataGet.Api via DataGetClient [query] (x1)
    └─ clients 1
      └─ DataGetClient
    └─ requests 1
      └─ GetCreditorsQuery
    └─ handlers 1
      └─ GetCreditorsQueryHandler
    └─ mappings 1
      └─ Creditors

