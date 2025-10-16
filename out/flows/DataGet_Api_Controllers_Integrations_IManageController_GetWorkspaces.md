[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/workspaces  (DataGet.Api.Controllers.Integrations.IManageController.GetWorkspaces)  [L197–L206] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageWorkspacesQuery [L205]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageWorkspacesQueryHandler.Handle [L25–L43]
      └─ uses_service IManageService
        └─ method GetWorkspaces [L36]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetWorkspaces [L12-L223]

