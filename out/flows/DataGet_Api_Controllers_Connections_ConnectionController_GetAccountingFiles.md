[web] GET /api/connection/{apiType}/accounting-files  (DataGet.Api.Controllers.Connections.ConnectionController.GetAccountingFiles)  [L100–L108] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetAccountingFilesQuery [L104]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountingFilesQueryHandler.Handle [L15–L38]
      └─ uses_client DataGetClient [L26]
        └─ calls GetAccountingFiles (GET /api/connection/{apiType}/accounting-files, method=GetAccountingFilesAsync, target=DataGet.Api) [L52]
          └─ target_service DataGet.Api
      └─ uses_service DataGetClient
        └─ method GetAccountingFilesAsync [L26]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountingFilesAsync [L15-L302]
            └─ calls GetAccountingFiles [L74]

