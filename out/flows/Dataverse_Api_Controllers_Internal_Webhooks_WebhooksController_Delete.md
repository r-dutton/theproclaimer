[web] DELETE /api/internal/webhooks/{hookId:Guid}  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.Delete)  [L76–L84] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls WebhookRepository.WriteQuery [L79]
  └─ writes_to Webhook [L79]
    └─ reads_from Webhooks
  └─ uses_service IControlledRepository<Webhook>
    └─ method WriteQuery [L79]

