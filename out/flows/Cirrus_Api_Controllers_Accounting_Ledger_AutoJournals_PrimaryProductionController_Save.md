[web] PUT /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/post-journal  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.Save)  [L107–L113] [auth=user]
  └─ sends_request ProcessPrimaryProductionJournalCommand [L110]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
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
      └─ uses_service IControlledRepository<Journal>
        └─ method Remove [L97]
      └─ uses_service IControlledRepository<PrimaryProductionConfig>
        └─ method WriteQuery [L159]
      └─ uses_service IControlledRepository<TradingAccount>
        └─ method ReadQuery [L73]
      └─ uses_service IMapper
        └─ method Map [L251]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L71]

