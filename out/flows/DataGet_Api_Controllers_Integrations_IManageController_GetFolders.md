[web] GET /api/imanage/customers/{customerId:int}/folders  (DataGet.Api.Controllers.Integrations.IManageController.GetFolders)  [L228–L235] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageFolderQuery -> GetIManageFolderQueryHandler [L234]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageFolderQueryHandler.Handle [L19–L32]
      └─ uses_service IManageService
        └─ method GetFolders [L28]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetFolders [L12-L223]
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
      └─ GetIManageFolderQuery
    └─ handlers 1
      └─ GetIManageFolderQueryHandler

