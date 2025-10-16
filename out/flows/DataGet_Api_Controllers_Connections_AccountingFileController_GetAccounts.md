[web] GET /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts)  [L44–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetAccountsQuery [L48]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
      └─ uses_client DataGetClient [L40]
        └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
          └─ target_service DataGet.Api
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L38]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
      └─ uses_service DataGetClient
        └─ method GetAccountsAsync [L40]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountsAsync [L15-L302]
            └─ calls GetAccounts [L106]

