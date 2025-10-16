[web] DELETE /api/fyi/disconnect  (DataGet.Api.Controllers.Integrations.FyiController.Disconnect)  [L283–L291] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method DeleteCurrentConnectionAsync [L287]
      └─ implementation IConnectionService.DeleteCurrentConnectionAsync [L44-L45]
      └─ ... (no implementation details available)

