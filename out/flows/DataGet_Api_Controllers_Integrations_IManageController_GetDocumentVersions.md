[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/documents/{documentId}/versions  (DataGet.Api.Controllers.Integrations.IManageController.GetDocumentVersions)  [L280–L288] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageDocumentVersionsQuery -> GetIManageDocumentVersionsQueryHandler [L287]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentVersionsQueryHandler.Handle [L20–L32]
      └─ uses_service IManageService
        └─ method GetVersions [L29]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetVersions [L12-L223]
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
      └─ GetIManageDocumentVersionsQuery
    └─ handlers 1
      └─ GetIManageDocumentVersionsQueryHandler

