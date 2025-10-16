[web] PUT /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/post-journal  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.Save)  [L107–L113] status=200 [auth=user]
  └─ sends_request ProcessPrimaryProductionJournalCommand -> ProcessPrimaryProductionJournalCommandHandler [L110]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessPrimaryProductionJournalCommandHandler.Handle [L46–L446]
      └─ maps_to SourceAccountInJournalModel [L326]
        └─ converts_to SourceAccountLightDto [L265]
      └─ uses_service IControlledRepository<PrimaryProductionConfig> (Scoped (inferred))
        └─ method WriteQuery [L159]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AutoJournals.PrimaryProductionConfigRepository.WriteQuery
      └─ uses_service IControlledRepository<Journal> (Scoped (inferred))
        └─ method Remove [L97]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.JournalRepository.Remove
      └─ uses_service IControlledRepository<TradingAccount> (Scoped (inferred))
        └─ method ReadQuery [L73]
          └─ implementation Cirrus.Data.Repository.Accounting.Setup.TradingAccountRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L71]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L68]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ ProcessPrimaryProductionJournalCommand
    └─ handlers 1
      └─ ProcessPrimaryProductionJournalCommandHandler
    └─ mappings 1
      └─ SourceAccountInJournalModel

