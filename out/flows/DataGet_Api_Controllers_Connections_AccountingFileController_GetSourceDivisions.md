[web] GET /api/accounting-file/{fileId}/divisions  (DataGet.Api.Controllers.Connections.AccountingFileController.GetSourceDivisions)  [L34–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDivisionsQuery -> GetDivisionsQueryHandler [L38]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetDivisionsQueryHandler.Handle [L22–L54]
      └─ maps_to SourceDivisionDto [L49]
      └─ uses_client DataGetClient [L45]
      └─ uses_service DataGetClient
        └─ method GetDivisionsAsync [L45]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetDivisionsAsync [L15-L302]
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
      └─ uses_service IControlledRepository<SourceDivision> (Scoped (inferred))
        └─ method ReadQuery [L41]
          └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceDivisionRepository.ReadQuery
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L39]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
            └─ uses_service IControlledRepository<Source> (Scoped (inferred))
              └─ method GetFileIdFromSource [L25]
                └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.GetFileIdFromSource
  └─ impact_summary
    └─ clients 1
      └─ DataGetClient
    └─ requests 1
      └─ GetDivisionsQuery
    └─ handlers 1
      └─ GetDivisionsQueryHandler
    └─ mappings 1
      └─ SourceDivisionDto

