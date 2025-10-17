[web] PUT /api/internal/webhooks/{hookId:guid}/error  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.SetErrorMessage)  [L66–L74] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls WebhookRepository.WriteQuery [L69]
  └─ write Webhook [L69]
    └─ reads_from Webhooks
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Webhook writes=1 reads=0

