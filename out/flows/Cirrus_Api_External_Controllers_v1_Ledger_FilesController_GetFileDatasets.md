[web] GET /ledger/v1/files/{fileId:Guid}/datasets  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.GetFileDatasets)  [L80–L87] status=200
  └─ maps_to DatasetDto [L83]
    └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
    └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
  └─ calls DatasetRepository.ReadQuery [L83]
  └─ query Dataset [L83]
  └─ uses_service IControlledRepository<Dataset>
    └─ method ReadQuery [L83]
      └─ ... (no implementation details available)

