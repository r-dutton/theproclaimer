[web] GET /webhooks/v1/webhooks/  (Dataverse.Api.External.Controllers.v1.Core.WebhooksController.FullQuery)  [L85–L92] [auth=Authentication.CoreRead]
  └─ calls WebhookRepository.ReadQuery [L90]
  └─ queries Webhook [L90]
    └─ reads_from Webhooks
  └─ uses_service IControlledRepository<Webhook>
    └─ method ReadQuery [L90]

