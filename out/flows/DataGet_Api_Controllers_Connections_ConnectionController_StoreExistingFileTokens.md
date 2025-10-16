[web] POST /api/connection/{apiType}/file-token  (DataGet.Api.Controllers.Connections.ConnectionController.StoreExistingFileTokens)  [L141–L148] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request StoreExistingFileTokensCommand [L145]
    └─ handled_by DataGet.Connections.App.Commands.StoreExistingFileTokensCommandHandler.Handle [L24–L79]
      └─ uses_service FileTokenService
        └─ method GetTokenAsync [L41]
          └─ implementation DataGet.Connections.App.Services.FileTokenService.GetTokenAsync [L11-L102]
          └─ implementation DataGet.Connections.App.Services.FileTokenService.GetTokenAsync [L11-L102]
      └─ uses_service IControlledRepository<FileToken>
        └─ method ReadQuery [L52]
          └─ ... (no implementation details available)
      └─ uses_service ILogger<StoreExistingFileTokensCommandHandler>
        └─ method LogError [L48]
          └─ ... (no implementation details available)
      └─ logs ILogger<StoreExistingFileTokensCommandHandler> [Error] [L48]

