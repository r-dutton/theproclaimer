[web] POST /api/internal/webhooks/  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.Create)  [L52–L57] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateWebhookCommand [L55]
    └─ handled_by Dataverse.ApplicationService.Commands.Webhooks.CreateWebhookCommandHandler.Handle [L15–L46]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L30]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service IControlledRepository<Webhook>
        └─ method Add [L42]
          └─ ... (no implementation details available)

