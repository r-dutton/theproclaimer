[web] POST /api/central/user/core  (Cirrus.Api.Controllers.Central.CentralController.ProcessIdentityAccountChanges)  [L37–L42] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request UpdateIdentityAccountChangesCommand [L40]
    └─ handled_by Cirrus.Central.Commands.UpdateIdentityAccountChangesCommandHandler.Handle [L28–L75]
      └─ calls CentralRepository.Commit [L61]
      └─ calls CentralRepository.WriteTable [L43]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L66]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

