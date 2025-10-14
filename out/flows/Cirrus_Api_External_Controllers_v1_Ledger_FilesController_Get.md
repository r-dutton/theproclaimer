[web] GET /ledger/v1/files/{fileId:Guid}  (Cirrus.Api.External.Controllers.v1.Ledger.FilesController.Get)  [L67–L72]
  └─ maps_to FileDto [L70]
    └─ automapper.registration CirrusMappingProfile (File->FileDto) [L199]
    └─ automapper.registration ExternalApiMappingProfile (File->FileDto) [L39]
  └─ calls FileRepository.ReadQuery [L70]
  └─ queries File [L70]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L70]

