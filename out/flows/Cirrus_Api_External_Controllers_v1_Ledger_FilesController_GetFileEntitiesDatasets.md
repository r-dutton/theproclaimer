[web] GET /ledger/v1/files/{fileId:Guid}/entities/{entityId:Guid}/datasets  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFileEntitiesDatasets)  [L113–L120] status=200
  └─ maps_to DatasetDto [L116]
    └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
    └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
  └─ calls DatasetRepository.ReadQuery [L116]
  └─ query Dataset [L116]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Dataset writes=0 reads=1
    └─ mappings 1
      └─ DatasetDto

