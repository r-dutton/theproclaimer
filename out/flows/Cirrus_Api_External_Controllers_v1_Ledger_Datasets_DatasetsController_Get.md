[web] GET /ledger/v1/datasets/{datasetId:Guid}  (Cirrus.Api.External.Controllers.v1.Ledger.Datasets.DatasetsController.Get)  [L79–L84] status=200
  └─ maps_to DatasetDto [L82]
    └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
    └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
  └─ calls DatasetRepository.ReadQuery [L82]
  └─ query Dataset [L82]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Dataset writes=0 reads=1
    └─ mappings 1
      └─ DatasetDto

