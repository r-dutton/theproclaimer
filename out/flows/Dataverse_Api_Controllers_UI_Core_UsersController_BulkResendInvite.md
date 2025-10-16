[web] POST /api/ui/users/bulk/invite  (Dataverse.Api.Controllers.UI.Core.UsersController.BulkResendInvite)  [L292–L305] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request ProcessInviteCommand -> ProcessInviteCommandHandler [L300]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ProcessInviteCommandHandler.Handle [L29–L65]
      └─ calls UserRepository (methods: WriteQuery,ReadQuery) [L57]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L53]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ ProcessInviteCommand
    └─ handlers 1
      └─ ProcessInviteCommandHandler

