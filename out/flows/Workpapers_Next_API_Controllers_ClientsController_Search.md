[web] GET /api/clients/  (Workpapers.Next.API.Controllers.ClientsController.Search)  [L45–L66] [auth=AuthorizationPolicies.User]
  └─ sends_request GetClientsQuery [L61]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Clients.GetClientsQueryHandler.Handle [L70–L165]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L88]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L90]
      └─ uses_service UserService
        └─ method GetUserId [L87]

