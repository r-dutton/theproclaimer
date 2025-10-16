[web] GET /webhooks/v1/webhooks/{webhookId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.WebhooksController.Get)  [L57–L62] status=200 [auth=Authentication.CoreRead]
  └─ maps_to WebhookDto [L60]
    └─ automapper.registration DataverseMappingProfile (Webhook->WebhookDto) [L458]
    └─ automapper.registration ExternalApiMappingProfile (Webhook->WebhookDto) [L181]
  └─ calls WebhookRepository.ReadQuery [L60]
  └─ query Webhook [L60]
    └─ reads_from Webhooks
  └─ uses_service IControlledRepository<Webhook>
    └─ method ReadQuery [L60]
      └─ ... (no implementation details available)

