[web] PUT /api/internal/webhooks/{hookId:guid}/error  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.SetErrorMessage)  [L66–L74] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls WebhookRepository.WriteQuery [L69]
  └─ write Webhook [L69]
    └─ reads_from Webhooks
  └─ uses_service IControlledRepository<Webhook>
    └─ method WriteQuery [L69]
      └─ ... (no implementation details available)

