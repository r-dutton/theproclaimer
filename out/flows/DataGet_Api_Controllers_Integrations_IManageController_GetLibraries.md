[web] GET /api/imanage/customers/{customerId:int}/libraries  (DataGet.Api.Controllers.Integrations.IManageController.GetLibraries)  [L179–L184] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IIManageService (AddScoped)
    └─ method GetLibraries [L183]
      └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetLibraries [L12-L223]
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
    └─ services 1
      └─ IIManageService

