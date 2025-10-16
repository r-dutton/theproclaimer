[web] PUT /api/connections/myob/es/trialbalance  (Workpapers.Next.API.Controllers.Connections.MYOBEssentialsController.GetTrialBalance)  [L42–L48] status=200
  └─ uses_service UserService
    └─ method GetUserId [L45]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
  └─ sends_request GetTrialBalanceQuery [L45]
    └─ handled_by Cirrus.Api.External.Queries.GetTrialBalanceQueryHandler.Handle [L44–L156]
      └─ uses_service GetAccountsQuery
        └─ method Execute [L73]
          └─ implementation Cirrus.Connections.DataGet.Queries.GetAccountsQuery.Execute [L9-L23]
            └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
              └─ uses_client DataGetClient [L40]
                └─ calls GetAccounts (GET /api/accounting-file/{fileId}/accounts?apiType={*}&password={*}&username={*}, method=GetAccountsAsync, target=DataGet.Api, query=apiType={*}&password={*}&username={*}) [L65]
                  └─ target_service DataGet.Api
                    └─ [web] GET /api/accounting-file/{fileId}/accounts  (DataGet.Api.Controllers.Connections.AccountingFileController.GetAccounts)  [L44–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
                      └─ sends_request GetAccountsQuery [L48]
                        └─ handled_by Cirrus.Connections.DataGet.Queries.GetAccountsQueryHandler.Handle [L25–L59]
              └─ uses_service DatagetFileIdService
                └─ method GetFileIdFromSource [L38]
                  └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
              └─ uses_service DataGetClient
                └─ method GetAccountsAsync [L40]
                  └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetAccountsAsync [L15-L302]
                    └─ calls GetAccounts [L106]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method Execute [L93]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L65]
          └─ ... (no implementation details available)

