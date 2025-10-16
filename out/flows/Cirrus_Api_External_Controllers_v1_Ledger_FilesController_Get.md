[web] GET /ledger/v1/files/{fileId:Guid}  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.Get)  [L67–L72] status=200
  └─ maps_to FileDto [L70]
    └─ automapper.registration CirrusMappingProfile (File->FileDto) [L199]
    └─ automapper.registration ExternalApiMappingProfile (File->FileDto) [L39]
  └─ calls FileRepository.ReadQuery [L70]
  └─ query File [L70]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L70]
      └─ ... (no implementation details available)

