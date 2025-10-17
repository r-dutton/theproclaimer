[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/folders/{folderId}/children  (DataGet.Api.Controllers.Integrations.IManageController.GetSubFolders)  [L218–L226] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageSubFoldersQuery -> GetIManageSubFoldersQueryHandler [L225]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageSubFoldersQueryHandler.Handle [L21–L37]
      └─ uses_service IManageService
        └─ method GetSubFolders [L32]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetSubFolders [L12-L223]
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
      └─ GetIManageSubFoldersQuery
    └─ handlers 1
      └─ GetIManageSubFoldersQueryHandler

