[web] GET /api/clients/and-entities  (Cirrus.Api.Controllers.Firm.ClientsController.SearchClientAndEntity)  [L84–L88] [auth=user]
  └─ sends_request FindClientsAndEntitiesQuery [L87]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindClientsAndEntitiesQueryHandler.Handle [L55–L138]
      └─ maps_to ClientEntitySearchDto [L122]
        └─ automapper.registration CirrusMappingProfile (Client->ClientEntitySearchDto) [L148]
      └─ maps_to ClientEntitySearchDto [L108]
        └─ automapper.registration CirrusMappingProfile (Entity->ClientEntitySearchDto) [L175]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L75]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L122]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L108]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L115]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUser [L78]

