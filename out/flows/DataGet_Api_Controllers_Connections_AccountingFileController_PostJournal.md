[web] POST /api/accounting-file/{fileId}/journal  (DataGet.Api.Controllers.Connections.AccountingFileController.PostJournal)  [L79–L87] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request PostJournalCommand -> PostJournalCommandHandler [L83]
    └─ handled_by Cirrus.Connections.DataGet.Commands.PostJournalCommandHandler.Handle [L30–L135]
      └─ uses_client IDatagetClient [L82]
      └─ uses_service IDatagetClient (InstancePerLifetimeScope)
        └─ method CreateJournalAsync [L82]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.CreateJournalAsync [L15-L302]
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
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.CreateJournalAsync [L32-L506]
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
      └─ uses_service IControlledRepository<SourceDivision> (Scoped (inferred))
        └─ method ReadQuery [L76]
          └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceDivisionRepository.ReadQuery
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method WriteQuery [L56]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.WriteQuery
      └─ uses_service IDatagetFileIdService (InstancePerLifetimeScope)
        └─ method GetFileIdFromSource [L52]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
            └─ uses_service IControlledRepository<Source> (Scoped (inferred))
              └─ method GetFileIdFromSource [L25]
                └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.GetFileIdFromSource
  └─ returns JournalPostResponse [L83]
  └─ impact_summary
    └─ clients 1
      └─ IDatagetClient
    └─ requests 1
      └─ PostJournalCommand
    └─ handlers 1
      └─ PostJournalCommandHandler
    └─ mappings 1
      └─ JournalPostResponse

