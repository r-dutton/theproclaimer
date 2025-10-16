[web] GET /api/accounting-file/{fileId}/transactions  (DataGet.Api.Controllers.Connections.AccountingFileController.GetTransactions)  [L65–L77] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetTransactionsQuery [L73]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetTransactionsQueryHandler.Handle [L34–L73]
      └─ uses_client DataGetClient [L50]
        └─ calls GetTransactions (GET /api/accounting-file/{fileId}/transactions?apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}, method=GetTransactionsAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&startDate={*}&username={*}) [L116]
          └─ target_service DataGet.Api
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L48]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
      └─ uses_service DataGetClient
        └─ method GetTransactionsAsync [L50]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetTransactionsAsync [L15-L302]
            └─ calls GetTransactions [L151]

