[web] PUT /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/post-journal  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.Save)  [L107–L113] status=200 [auth=user]
  └─ sends_request ProcessPrimaryProductionJournalCommand [L110]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessPrimaryProductionJournalCommandHandler.Handle [L46–L446]
      └─ maps_to SourceAccountInJournalModel [L326]
        └─ converts_to SourceAccountLightDto [L265]
      └─ maps_to SourceAccountInJournalModel [L309]
        └─ converts_to SourceAccountLightDto [L265]
      └─ maps_to SourceAccountInJournalModel [L270]
        └─ converts_to SourceAccountLightDto [L265]
      └─ maps_to SourceAccountInJournalModel [L251]
        └─ converts_to SourceAccountLightDto [L265]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Journal>
        └─ method Remove [L97]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<PrimaryProductionConfig>
        └─ method WriteQuery [L159]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<TradingAccount>
        └─ method ReadQuery [L73]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L251]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L71]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

