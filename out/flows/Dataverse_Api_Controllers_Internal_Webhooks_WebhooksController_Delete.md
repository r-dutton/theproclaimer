[web] DELETE /api/internal/webhooks/{hookId:Guid}  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.Delete)  [L76–L84] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls WebhookRepository.WriteQuery [L79]
  └─ write Webhook [L79]
    └─ reads_from Webhooks
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Webhook writes=1 reads=0

