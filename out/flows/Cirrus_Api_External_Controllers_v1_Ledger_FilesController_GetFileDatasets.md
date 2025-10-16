[web] GET /ledger/v1/files/{fileId:Guid}/datasets  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFileDatasets)  [L80–L87] status=200
  └─ maps_to DatasetDto [L83]
    └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
    └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
  └─ calls DatasetRepository.ReadQuery [L83]
  └─ query Dataset [L83]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Dataset writes=0 reads=1
    └─ mappings 1
      └─ DatasetDto

