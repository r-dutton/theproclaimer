[web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.Get)  [L47–L51] status=200 [auth=user]
  └─ sends_request GetPrimaryProductionDtoQuery -> GetPrimaryProductionInfoQueryHandler [L50]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetPrimaryProductionInfoQueryHandler.Handle [L34–L79]
      └─ maps_to PrimaryProductionConfigDto [L62]
        └─ automapper.registration CirrusMappingProfile (PrimaryProductionConfig->PrimaryProductionConfigDto) [L501]
      └─ uses_service IControlledRepository<PrimaryProductionConfig> (Scoped (inferred))
        └─ method ReadQuery [L62]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AutoJournals.PrimaryProductionConfigRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L56]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L51]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetPrimaryProductionDtoQuery
    └─ handlers 1
      └─ GetPrimaryProductionInfoQueryHandler
    └─ mappings 1
      └─ PrimaryProductionConfigDto

