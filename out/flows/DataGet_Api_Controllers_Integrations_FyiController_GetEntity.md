[web] GET /api/fyi/entities/{entityId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetEntity)  [L186–L195] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetEntityQuery [L191]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetEntityQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetEntity [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetEntity [L30-L166]

