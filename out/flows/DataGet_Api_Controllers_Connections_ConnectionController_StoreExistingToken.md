[web] POST /api/connection/{apiType}/token  (DataGet.Api.Controllers.Connections.ConnectionController.StoreExistingToken)  [L132–L139] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request StoreExistingTokenCommand -> StoreExistingTokenCommandHandler [L136]
    └─ handled_by DataGet.Connections.App.Commands.StoreExistingTokenCommandHandler.Handle [L17–L34]
      └─ uses_service UserTokenService
        └─ method GetTokenAsync [L28]
          └─ implementation DataGet.Connections.App.Services.UserTokenService.GetTokenAsync [L11-L81]
            └─ uses_service ConnectionContextProvider
              └─ method GetTokenAsync [L28]
                └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ StoreExistingTokenCommand
    └─ handlers 1
      └─ StoreExistingTokenCommandHandler

