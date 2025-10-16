[web] GET /api/accounting-file/{fileId}/wages  (DataGet.Api.Controllers.Connections.AccountingFileController.GetWages)  [L129–L145] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetWagesQuery [L134]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetWagesQueryHandler.Handle [L26–L45]
      └─ uses_client DataGetClient [L41]
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, method=GetWagesAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L189]
          └─ target_service DataGet.Api
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, method=GetWagesAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L190]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, method=GetWagesAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L191]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, method=GetWagesAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L192]
          └─ target_service DataGet.Api (see previous expansion)
        └─ calls GetWages (GET /api/accounting-file/{fileId}/wages?apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}, method=GetWagesAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&jurisdiction={*}&password={*}&startDate={*}&username={*}) [L193]
          └─ target_service DataGet.Api (see previous expansion)
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L39]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
      └─ uses_service DataGetClient
        └─ method GetWagesAsync [L41]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetWagesAsync [L15-L302]
            └─ calls GetWages [L218]

