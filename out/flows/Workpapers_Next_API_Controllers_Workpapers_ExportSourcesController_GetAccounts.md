[web] GET /api/sources/export/{id:guid}/accounts  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetAccounts)  [L68–L73] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetExportSourceAccountsQuery [L71]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourceAccountsQueryHandler.Handle [L29–L68]
      └─ uses_client DataGetClient [L46]
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
      └─ uses_service DataGetClient
        └─ method GetAccountsAsync [L46]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetAccountsAsync [L32-L506]
            └─ calls GetAccounts [L65]
      └─ uses_service IControlledRepository<ExportSource>
        └─ method ReadQuery [L42]
          └─ ... (no implementation details available)

