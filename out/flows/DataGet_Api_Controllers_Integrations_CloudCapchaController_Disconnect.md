[web] DELETE /api/cloud-capcha/disconnect  (DataGet.Api.Controllers.Integrations.CloudCapchaController.Disconnect)  [L118–L126] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method DeleteCurrentConnectionAsync [L123]
      └─ implementation IConnectionService.DeleteCurrentConnectionAsync [L44-L45]
      └─ ... (no implementation details available)

