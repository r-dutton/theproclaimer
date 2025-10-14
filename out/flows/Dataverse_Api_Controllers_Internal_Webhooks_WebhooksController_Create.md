[web] POST /api/internal/webhooks/  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.Create)  [L52–L57] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateWebhookCommand [L55]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Webhooks.CreateWebhookCommandHandler.Handle [L15–L46]
      └─ uses_service IControlledRepository<Webhook>
        └─ method Add [L42]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L30]

