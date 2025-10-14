[web] POST /api/connection/{apiType}/token  (DataGet.Api.Controllers.Connections.ConnectionController.StoreExistingToken)  [L132–L139] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request StoreExistingTokenCommand [L136]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Commands.StoreExistingTokenCommandHandler.Handle [L17–L34]
      └─ uses_service UserTokenService
        └─ method GetTokenAsync [L28]

