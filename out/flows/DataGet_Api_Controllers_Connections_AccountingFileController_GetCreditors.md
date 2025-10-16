[web] GET /api/accounting-file/{fileId}/creditors  (DataGet.Api.Controllers.Connections.AccountingFileController.GetCreditors)  [L109–L117] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCreditorsQuery [L113]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetCreditorsQueryHandler.Handle [L25–L57]
      └─ uses_client DataGetClient [L53]
        └─ calls GetCreditors (GET /api/accounting-file/{fileId}/creditors?apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}, method=GetCreditorsAsync, target=DataGet.Api, query=apiType={*}&endDate={*}&password={*}&sourceAccountSystemId={*}&username={*}) [L156]
          └─ target_service DataGet.Api
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L40]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
      └─ uses_service DataGetClient
        └─ method GetCreditorsAsync [L53]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetCreditorsAsync [L15-L302]
            └─ calls GetCreditors [L194]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L46]
          └─ ... (no implementation details available)
  └─ returns Creditors [L113]

