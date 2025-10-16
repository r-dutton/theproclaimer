[web] GET /api/clients/{id}  (Cirrus.Api.Controllers.Firm.ClientsController.Get)  [L93–L111] status=200 [auth=user]
  └─ maps_to ClientDto [L108]
    └─ automapper.registration DataverseMappingProfile (Client->ClientDto) [L165]
    └─ automapper.registration WorkpapersMappingProfile (Client->ClientDto) [L69]
    └─ automapper.registration CirrusMappingProfile (Client->ClientDto) [L146]
  └─ maps_to ClientDto [L98]
    └─ automapper.registration DataverseMappingProfile (Client->ClientDto) [L165]
    └─ automapper.registration WorkpapersMappingProfile (Client->ClientDto) [L69]
    └─ automapper.registration CirrusMappingProfile (Client->ClientDto) [L146]
  └─ calls ClientRepository.ReadQuery [L98]
  └─ query Client [L98]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L98]
      └─ ... (no implementation details available)

