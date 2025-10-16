[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/clients  (DataGet.Api.Controllers.Integrations.IManageController.GetClients)  [L186–L195] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageClientsQuery -> GetIManageClientsQueryHandler [L194]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageClientsQueryHandler.Handle [L24–L42]
      └─ uses_service IManageService
        └─ method GetClients [L35]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetClients [L12-L223]
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
      └─ GetIManageClientsQuery
    └─ handlers 1
      └─ GetIManageClientsQueryHandler

