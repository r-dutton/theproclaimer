[web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/account-balance/{sourceAccountId}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.GetAccountBalance)  [L57–L62] status=200 [auth=user]
  └─ sends_request GetPrimaryProductionRowsDtoQuery [L60]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetPrimaryProductionRowsDtoQueryHandler.Handle [L47–L147]
      └─ maps_to PrimaryProductionConfigDto [L137]
        └─ automapper.registration CirrusMappingProfile (PrimaryProductionConfig->PrimaryProductionConfigDto) [L501]
      └─ maps_to SourceAccountForPrimaryProductionDto [L79]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountForPrimaryProductionDto) [L276]
        └─ converts_to SourceAccountInJournalModel [L280]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L88]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L126]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<PrimaryProductionConfig>
        └─ method ReadQuery [L137]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L79]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L83]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L131]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

