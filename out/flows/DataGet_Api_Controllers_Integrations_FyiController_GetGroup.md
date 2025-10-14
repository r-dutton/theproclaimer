[web] GET /api/fyi/groups/{groupId:long}  (DataGet.Api.Controllers.Integrations.FyiController.GetGroup)  [L208–L215] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetGroupQuery [L211]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetGroupQueryHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method GetGroup [L29]

