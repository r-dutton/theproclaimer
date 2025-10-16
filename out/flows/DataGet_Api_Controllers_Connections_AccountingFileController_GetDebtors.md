[web] GET /api/accounting-file/{fileId}/debtors  (DataGet.Api.Controllers.Connections.AccountingFileController.GetDebtors)  [L119–L127] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDebtorsQuery [L123]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetDebtorsQueryHandler.Handle [L25–L56]
      └─ uses_client DataGetClient [L52]
        └─ calls GetDebtors (GET /api/accounting-file/{fileId}/debtors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, method=GetDebtorsAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L171]
          └─ target_service DataGet.Api
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L40]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
      └─ uses_service DataGetClient
        └─ method GetDebtorsAsync [L52]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetDebtorsAsync [L15-L302]
            └─ calls GetDebtors [L206]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L45]
          └─ ... (no implementation details available)
  └─ returns Debtors [L123]

