[web] GET /api/fyi/cabinets  (DataGet.Api.Controllers.Integrations.FyiController.GetCabinets)  [L45–L54] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCabinetsQuery [L50]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetCabinetsQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetCabinets [L30]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetCabinets [L30-L166]

