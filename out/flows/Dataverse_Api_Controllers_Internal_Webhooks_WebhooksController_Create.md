[web] POST /api/internal/webhooks/  (Dataverse.Api.Controllers.Internal.Webhooks.WebhooksController.Create)  [L52–L57] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateWebhookCommand -> CreateWebhookCommandHandler [L55]
    └─ handled_by Dataverse.ApplicationService.Commands.Webhooks.CreateWebhookCommandHandler.Handle [L15–L46]
      └─ uses_service IControlledRepository<Webhook> (Scoped (inferred))
        └─ method Add [L42]
          └─ implementation Dataverse.Data.Repository.Webhooks.WebhookRepository.Add
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L30]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
            └─ uses_service RequestInfo
              └─ method IsValidServiceAccountRequest [L82]
                └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L71]
                      └─ ... (service recursion detected)
                  └─ logs ILogger<IRequestInfoService> [Warning] [L81]
            └─ logs ILogger<IRequestInfoService> [Warning] [L89]
  └─ impact_summary
    └─ requests 1
      └─ CreateWebhookCommand
    └─ handlers 1
      └─ CreateWebhookCommandHandler

