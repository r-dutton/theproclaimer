[web] GET /api/clients/{id}/entities  (Cirrus.Api.Controllers.Firm.ClientsController.GetEntitiesAndFiles)  [L120–L156] [auth=user]
  └─ maps_to FileDto [L145]
    └─ automapper.registration CirrusMappingProfile (File->FileDto) [L199]
    └─ automapper.registration ExternalApiMappingProfile (File->FileDto) [L39]
  └─ maps_to EntityWithFileDto [L138]
    └─ automapper.registration CirrusMappingProfile (Entity->EntityWithFileDto) [L172]
  └─ calls FileRepository.ReadQuery [L145]
  └─ calls EntityRepository.ReadQuery [L138]
  └─ calls ClientRepository.ReadQuery [L132]
  └─ queries File [L145]
    └─ reads_from Files
  └─ queries Client [L132]
  └─ queries Entity [L138]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L132]
  └─ uses_service IControlledRepository<Entity>
    └─ method ReadQuery [L138]
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L145]

