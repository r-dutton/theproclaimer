[web] GET /api/fyi/groups/{groupId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetGroup)  [L208–L215] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetGroupQuery -> GetGroupQueryHandler [L211]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetGroupQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetGroup [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetGroup [L30-L166]
            └─ uses_client FyiHttpClient [L50]
            └─ uses_service FyiHttpClient
              └─ method GetCabinets [L50]
                └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
  └─ impact_summary
    └─ clients 1
      └─ FyiHttpClient
    └─ requests 1
      └─ GetGroupQuery
    └─ handlers 1
      └─ GetGroupQueryHandler

