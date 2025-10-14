[web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.Get)  [L47–L51] [auth=user]
  └─ sends_request GetPrimaryProductionDtoQuery [L50]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetPrimaryProductionInfoQueryHandler.Handle [L34–L79]
      └─ maps_to PrimaryProductionConfigDto [L62]
        └─ automapper.registration CirrusMappingProfile (PrimaryProductionConfig->PrimaryProductionConfigDto) [L501]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L51]
      └─ uses_service IControlledRepository<PrimaryProductionConfig>
        └─ method ReadQuery [L62]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L56]

