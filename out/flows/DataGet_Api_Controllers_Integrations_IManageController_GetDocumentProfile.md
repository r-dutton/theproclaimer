[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/documents/{documentId}/profile  (DataGet.Api.Controllers.Integrations.IManageController.GetDocumentProfile)  [L270–L278] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageDocumentProfileQuery -> GetIManageDocumentProfileQueryHandler [L277]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentProfileQueryHandler.Handle [L24–L40]
      └─ uses_service IManageService
        └─ method GetDocumentProfile [L35]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetDocumentProfile [L12-L223]
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
      └─ GetIManageDocumentProfileQuery
    └─ handlers 1
      └─ GetIManageDocumentProfileQueryHandler

