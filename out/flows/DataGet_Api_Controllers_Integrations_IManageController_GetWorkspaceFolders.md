[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/workspaces/{workspaceId}/children  (DataGet.Api.Controllers.Integrations.IManageController.GetWorkspaceFolders)  [L208–L216] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageWorkspaceChildrenQuery -> GetIManageWorkspaceChildrenQueryHandler [L215]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageWorkspaceChildrenQueryHandler.Handle [L22–L38]
      └─ uses_service IManageService
        └─ method GetWorkspaceFolders [L33]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetWorkspaceFolders [L12-L223]
            └─ uses_client IManageApiClient [L33]
            └─ uses_service IManageApiClient
              └─ method GetApiInformation [L33]
                └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation [L17-L95]
            └─ uses_service RequestProcessor
              └─ method GetAuthorisationUrl [L28]
                └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                └─ +1 additional_requests elided
  └─ impact_summary
    └─ clients 1
      └─ IManageApiClient
    └─ requests 1
      └─ GetIManageWorkspaceChildrenQuery
    └─ handlers 1
      └─ GetIManageWorkspaceChildrenQueryHandler

