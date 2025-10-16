[web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.Get)  [L47–L51] status=200 [auth=user]
  └─ sends_request GetPrimaryProductionDtoQuery [L50]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetPrimaryProductionInfoQueryHandler.Handle [L34–L79]
      └─ maps_to PrimaryProductionConfigDto [L62]
        └─ automapper.registration CirrusMappingProfile (PrimaryProductionConfig->PrimaryProductionConfigDto) [L501]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L51]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<PrimaryProductionConfig>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L66]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L56]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

