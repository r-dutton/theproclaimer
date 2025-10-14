[web] GET /api/accounting/files/for-client/{clientId}  (Cirrus.Api.Controllers.Accounting.FilesController.GetForClient)  [L103–L128] [auth=user]
  └─ maps_to FileLightDto [L122]
    └─ automapper.registration CirrusMappingProfile (File->FileLightDto) [L192]
  └─ calls FileRepository.ReadQuery [L122]
  └─ calls ClientRepository.ReadQuery [L112]
  └─ queries File [L122]
    └─ reads_from Files
  └─ queries Client [L112]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L112]
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L122]

