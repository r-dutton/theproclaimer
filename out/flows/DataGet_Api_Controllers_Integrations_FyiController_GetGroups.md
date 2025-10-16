[web] GET /api/fyi/groups  (DataGet.Api.Controllers.Integrations.FyiController.GetGroups)  [L197–L206] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetGroupsQuery [L202]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetGroupsQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetGroups [L30]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetGroups [L30-L166]

