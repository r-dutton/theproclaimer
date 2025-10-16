[web] DELETE /api/connection/{apiType}/disconnect-file  (DataGet.Api.Controllers.Connections.ConnectionController.DisconnectFile)  [L150–L157] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request DisconnectFileCommand [L154]
    └─ handled_by Cirrus.Connections.DataGet.Commands.DisconnectFileCommandHandler.Handle [L21–L35]
      └─ uses_client IDatagetClient [L32]
      └─ uses_service IDatagetClient (InstancePerLifetimeScope)
        └─ method DisconnectFileAsync [L32]
          └─ ... (no implementation details available)

