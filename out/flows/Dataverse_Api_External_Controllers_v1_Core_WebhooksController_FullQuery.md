[web] GET /webhooks/v1/webhooks/  (Dataverse.Api.External.Controllers.v1.Core.WebhooksController.FullQuery)  [L85–L92] status=200 [auth=Authentication.CoreRead]
  └─ calls WebhookRepository.ReadQuery [L90]
  └─ query Webhook [L90]
    └─ reads_from Webhooks
  └─ uses_service IControlledRepository<Webhook>
    └─ method ReadQuery [L90]
      └─ ... (no implementation details available)

