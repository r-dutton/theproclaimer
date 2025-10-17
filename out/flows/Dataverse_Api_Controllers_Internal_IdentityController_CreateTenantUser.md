[web] POST /api/internal/identity  (Dataverse.Api.Controllers.Internal.IdentityController.CreateTenantUser)  [L57–L63] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request CreateCentralUserCommand -> CreateCentralUserCommandHandler [L60]
    └─ handled_by Dataverse.Tenants.Commands.Users.CreateCentralUserCommandHandler.Handle [L25–L61]
      └─ calls TenantRepository.Add [L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L41]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ CreateCentralUserCommand
    └─ handlers 1
      └─ CreateCentralUserCommandHandler

