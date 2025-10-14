[web] GET /api/fyi/groups  (DataGet.Api.Controllers.Integrations.FyiController.GetGroups)  [L197–L206] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetGroupsQuery [L202]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetGroupsQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetGroups [L30]

