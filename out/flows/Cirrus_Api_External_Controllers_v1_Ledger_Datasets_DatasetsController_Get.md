[web] GET /ledger/v1/datasets/{datasetId:Guid}  (Cirrus.Api.External.Controllers.v1.Ledger.Datasets.DatasetsController.Get)  [L79–L84]
  └─ maps_to DatasetDto [L82]
    └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
    └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
  └─ calls DatasetRepository.ReadQuery [L82]
  └─ queries Dataset [L82]
  └─ uses_service IControlledRepository<Dataset>
    └─ method ReadQuery [L82]

