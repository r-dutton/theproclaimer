[web] GET /api/clients/  (Workpapers.Next.API.Controllers.ClientsController.Search)  [L45–L66] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetClientsQuery [L61]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Clients.GetClientsQueryHandler.Handle [L70–L165]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L88]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
      └─ uses_service UserService
        └─ method GetUserId [L87]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L90]
          └─ ... (no implementation details available)

