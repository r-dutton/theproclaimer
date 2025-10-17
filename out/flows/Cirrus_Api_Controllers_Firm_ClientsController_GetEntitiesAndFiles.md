[web] GET /api/clients/{id}/entities  (Cirrus.Api.Controllers.Firm.ClientsController.GetEntitiesAndFiles)  [L120–L156] status=200 [auth=user]
  └─ maps_to FileDto [L145]
    └─ automapper.registration ExternalApiMappingProfile (File->FileDto) [L39]
    └─ automapper.registration CirrusMappingProfile (File->FileDto) [L199]
  └─ maps_to EntityWithFileDto [L138]
    └─ automapper.registration CirrusMappingProfile (Entity->EntityWithFileDto) [L172]
  └─ calls FileRepository.ReadQuery [L145]
  └─ calls EntityRepository.ReadQuery [L138]
  └─ calls ClientRepository.ReadQuery [L132]
  └─ query File [L145]
    └─ reads_from Files
  └─ query Entity [L138]
  └─ query Client [L132]
  └─ impact_summary
    └─ entities 3 (writes=0, reads=3)
      └─ Client writes=0 reads=1
      └─ Entity writes=0 reads=1
      └─ File writes=0 reads=1
    └─ mappings 2
      └─ EntityWithFileDto
      └─ FileDto

