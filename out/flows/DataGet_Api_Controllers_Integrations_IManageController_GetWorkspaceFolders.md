[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/workspaces/{workspaceId}/children  (DataGet.Api.Controllers.Integrations.IManageController.GetWorkspaceFolders)  [L208–L216] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageWorkspaceChildrenQuery [L215]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageWorkspaceChildrenQueryHandler.Handle [L22–L38]
      └─ uses_service IManageService
        └─ method GetWorkspaceFolders [L33]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetWorkspaceFolders [L12-L223]

