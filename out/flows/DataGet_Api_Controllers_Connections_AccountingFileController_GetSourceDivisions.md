[web] GET /api/accounting-file/{fileId}/divisions  (DataGet.Api.Controllers.Connections.AccountingFileController.GetSourceDivisions)  [L34–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetDivisionsQuery [L38]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetDivisionsQueryHandler.Handle [L22–L54]
      └─ maps_to SourceDivisionDto [L49]
      └─ uses_client DataGetClient [L45]
      └─ uses_service DatagetFileIdService
        └─ method GetFileIdFromSource [L39]
          └─ implementation Cirrus.Connections.DataGet.Services.DatagetFileIdService.GetFileIdFromSource [L14-L37]
      └─ uses_service DataGetClient
        └─ method GetDivisionsAsync [L45]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetDivisionsAsync [L15-L302]
            └─ calls GetSourceDivisions [L95]
      └─ uses_service IControlledRepository<SourceDivision>
        └─ method ReadQuery [L41]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L49]
          └─ ... (no implementation details available)

