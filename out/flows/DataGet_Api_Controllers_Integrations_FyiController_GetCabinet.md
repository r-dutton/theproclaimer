[web] GET /api/fyi/cabinets/{cabinetId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetCabinet)  [L56–L65] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCabinetQuery [L61]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCabinetQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetCabinet [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCabinet [L30-L166]

