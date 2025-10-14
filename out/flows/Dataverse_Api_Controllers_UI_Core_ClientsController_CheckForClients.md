[web] GET /api/ui/clients/exist-for-firm  (Dataverse.Api.Controllers.UI.Core.ClientsController.CheckForClients)  [L129–L137] [auth=Authentication.UserPolicy]
  └─ calls ClientRepository.ReadQuery [L132]
  └─ queries Client [L132]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L132]

