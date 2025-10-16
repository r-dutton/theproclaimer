[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/workspaces  (DataGet.Api.Controllers.Integrations.IManageController.GetWorkspaces)  [L197–L206] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageWorkspacesQuery -> GetIManageWorkspacesQueryHandler [L205]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageWorkspacesQueryHandler.Handle [L25–L43]
      └─ uses_service IManageService
        └─ method GetWorkspaces [L36]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetWorkspaces [L12-L223]
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
      └─ GetIManageWorkspacesQuery
    └─ handlers 1
      └─ GetIManageWorkspacesQueryHandler

