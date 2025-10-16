[web] GET /api/clients/{id}  (Cirrus.Api.Controllers.Firm.ClientsController.Get)  [L93–L111] status=200 [auth=user]
  └─ maps_to ClientDto [L108]
    └─ automapper.registration CirrusMappingProfile (Client->ClientDto) [L146]
  └─ calls ClientRepository.ReadQuery [L98]
  └─ query Client [L98]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Client writes=0 reads=1
    └─ mappings 1
      └─ ClientDto

