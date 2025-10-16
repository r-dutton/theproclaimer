[web] GET /api/clients/and-entities  (Cirrus.Api.Controllers.Firm.ClientsController.SearchClientAndEntity)  [L84–L88] status=200 [auth=user]
  └─ sends_request FindClientsAndEntitiesQuery [L87]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindClientsAndEntitiesQueryHandler.Handle [L55–L138]
      └─ maps_to ClientEntitySearchDto [L122]
        └─ automapper.registration CirrusMappingProfile (Client->ClientEntitySearchDto) [L148]
      └─ maps_to ClientEntitySearchDto [L108]
        └─ automapper.registration CirrusMappingProfile (Entity->ClientEntitySearchDto) [L175]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L75]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L122]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L108]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L115]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUser [L78]
          └─ implementation IUserService.GetUser [L18-L18]
          └─ ... (no implementation details available)

