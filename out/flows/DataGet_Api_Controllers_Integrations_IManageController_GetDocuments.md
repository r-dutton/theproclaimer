[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/documents  (DataGet.Api.Controllers.Integrations.IManageController.GetDocuments)  [L237–L246] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageDocumentsQuery -> GetIManageDocumentsQueryHandler [L245]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageDocumentsQueryHandler.Handle [L26–L60]
      └─ uses_service IntegrationConfigService
        └─ method GetApiIntegrationConfig [L53]
          └─ implementation DataGet.Connections.App.Services.IntegrationConfigService.GetApiIntegrationConfig [L8-L37]
            └─ uses_service IControlledRepository<IntegrationConfiguration> (Scoped (inferred))
              └─ method GetApiIntegrationConfig [L19]
                └─ implementation DataGet.Data.Repository.Connections.IntegrationConfigRepository.GetApiIntegrationConfig [L5-L35]
      └─ uses_service IManageService
        └─ method GetDocuments [L41]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetDocuments [L12-L223]
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
      └─ GetIManageDocumentsQuery
    └─ handlers 1
      └─ GetIManageDocumentsQueryHandler

