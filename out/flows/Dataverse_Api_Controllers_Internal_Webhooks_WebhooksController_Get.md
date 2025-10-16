[web] GET /api/internal/webhooks/{hookId:Guid}  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.Get)  [L44–L50] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to WebhookDto [L47]
    └─ automapper.registration DataverseMappingProfile (Webhook->WebhookDto) [L458]
    └─ automapper.registration ExternalApiMappingProfile (Webhook->WebhookDto) [L181]
  └─ calls WebhookRepository.ReadQuery [L47]
  └─ query Webhook [L47]
    └─ reads_from Webhooks
  └─ uses_service IControlledRepository<Webhook>
    └─ method ReadQuery [L47]
      └─ ... (no implementation details available)

