[web] GET /api/accounting-file/{fileId}/movement-journals  (DataGet.Api.Controllers.Connections.AccountingFileController.GetMovements)  [L54–L63] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetMovementJournalsQuery [L59]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetMovementJournalsQueryHandler.Handle [L49–L204]
      └─ uses_client DataGetClient [L81]
        └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
          └─ target_service DataGet.Api
            └─ [web] GET /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts)  [L44–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
              └─ sends_request GetAccountsQuery [L48]
                └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
                  └─ uses_client DataGetClient [L40]
                    └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                      └─ target_service DataGet.Api (see previous expansion)
                  └─ uses_service DatagetFileIdService
                    └─ method GetFileIdFromSource [L38]
                      └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
                  └─ uses_service DataGetClient
                    └─ method GetAccountsAsync [L40]
                      └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountsAsync [L15-L302]
                        └─ calls GetAccounts [L106]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L77]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L76]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
      └─ uses_service DataGetClient
        └─ method GetAccountsAsync [L81]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountsAsync [L15-L302]
            └─ calls GetAccounts [L106]
      └─ uses_service IControlledRepository<CachedSourceAccount>
        └─ method Add [L116]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceDivision>
        └─ method ReadQuery [L187]
          └─ ... (no implementation details available)

