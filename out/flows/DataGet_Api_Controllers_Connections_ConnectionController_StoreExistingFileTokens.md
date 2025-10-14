[web] POST /api/connection/{apiType}/file-token  (DataGet.Api.Controllers.Connections.ConnectionController.StoreExistingFileTokens)  [L141–L148] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request StoreExistingFileTokensCommand [L145]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Commands.StoreExistingFileTokensCommandHandler.Handle [L24–L79]
      └─ uses_service FileTokenService
        └─ method GetTokenAsync [L41]
      └─ uses_service IControlledRepository<FileToken>
        └─ method ReadQuery [L52]
      └─ uses_service ILogger<StoreExistingFileTokensCommandHandler>
        └─ method LogError [L48]

