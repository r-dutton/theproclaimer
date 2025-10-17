[web] POST /api/connection/{apiType}/file-token  (DataGet.Api.Controllers.Connections.ConnectionController.StoreExistingFileTokens)  [L141–L148] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request StoreExistingFileTokensCommand -> StoreExistingFileTokensCommandHandler [L145]
    └─ handled_by DataGet.Connections.App.Commands.StoreExistingFileTokensCommandHandler.Handle [L24–L79]
      └─ calls FileTokenRepository.ReadQuery [L52]
      └─ uses_service FileTokenService
        └─ method GetTokenAsync [L41]
          └─ implementation DataGet.Connections.App.Services.FileTokenService.GetTokenAsync [L11-L102]
            └─ calls FileTokenRepository (methods: Remove,WriteQuery,Add,ReadQuery) [L90]
            └─ uses_service ConnectionContextProvider
              └─ method GetTokenConnectionId [L24]
                └─ ... (no implementation details available)
      └─ logs ILogger<StoreExistingFileTokensCommandHandler> [Error] [L48]
  └─ impact_summary
    └─ requests 1
      └─ StoreExistingFileTokensCommand
    └─ handlers 1
      └─ StoreExistingFileTokensCommandHandler

