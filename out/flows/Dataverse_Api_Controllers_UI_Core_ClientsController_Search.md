[web] GET /api/ui/clients/  (Dataverse.Api.Controllers.UI.Core.ClientsController.Search)  [L86–L109] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindClientsQuery [L108]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindClientsQueryHandler.Handle [L76–L187]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L108]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L96]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Office>
        └─ method ReadQuery [L115]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L99]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
  └─ sends_request FindClientsLightQuery [L107]

