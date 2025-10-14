[web] GET /api/ui/clients/  (Dataverse.Api.Controllers.UI.Core.ClientsController.Search)  [L86–L109] [auth=Authentication.UserPolicy]
  └─ sends_request FindClientsQuery [L108]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindClientsQueryHandler.Handle [L76–L187]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L108]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L96]
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L115]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L99]
  └─ sends_request FindClientsLightQuery [L107]

