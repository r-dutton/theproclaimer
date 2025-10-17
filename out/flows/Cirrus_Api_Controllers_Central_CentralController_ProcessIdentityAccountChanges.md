[web] POST /api/central/user/core  (Cirrus.Api.Controllers.Central.CentralController.ProcessIdentityAccountChanges)  [L37–L42] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request UpdateIdentityAccountChangesCommand -> UpdateIdentityAccountChangesCommandHandler [L40]
    └─ handled_by Cirrus.Central.Commands.UpdateIdentityAccountChangesCommandHandler.Handle [L28–L75]
      └─ calls CentralRepository (methods: Commit,WriteTable) [L61]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L66]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ UpdateIdentityAccountChangesCommand
    └─ handlers 1
      └─ UpdateIdentityAccountChangesCommandHandler

