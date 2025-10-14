[web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/account-balance/{sourceAccountId}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.GetAccountBalance)  [L57–L62] [auth=user]
  └─ sends_request GetPrimaryProductionRowsDtoQuery [L60]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetPrimaryProductionRowsDtoQueryHandler.Handle [L47–L147]
      └─ maps_to PrimaryProductionConfigDto [L137]
        └─ automapper.registration CirrusMappingProfile (PrimaryProductionConfig->PrimaryProductionConfigDto) [L501]
      └─ maps_to SourceAccountForPrimaryProductionDto [L79]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountForPrimaryProductionDto) [L276]
        └─ converts_to SourceAccountInJournalModel [L280]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L88]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L126]
      └─ uses_service IControlledRepository<PrimaryProductionConfig>
        └─ method ReadQuery [L137]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L79]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L83]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L131]

