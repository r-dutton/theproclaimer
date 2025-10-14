[web] PUT /api/internal/webhooks/{hookId:guid}/error  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.SetErrorMessage)  [L66–L74] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls WebhookRepository.WriteQuery [L69]
  └─ writes_to Webhook [L69]
    └─ reads_from Webhooks
  └─ uses_service IControlledRepository<Webhook>
    └─ method WriteQuery [L69]

