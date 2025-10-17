[web] POST /api/imanage/customers/{customerId:int}/libraries/{library}/client-selector/{clientSelector}/documents  (DataGet.Api.Controllers.Integrations.IManageController.UploadFile)  [L258–L268] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request UploadFileToIManageCommand -> UploadFileToIManageCommandHandler [L266]
    └─ handled_by DataGet.Integrations.iManage.Api.Command.UploadFileToIManageCommandHandler.Handle [L27–L83]
      └─ calls IntegrationConfigRepository.ReadQuery [L66]
      └─ uses_client HttpClient [L57]
      └─ uses_service IManageService
        └─ method UploadFileToIManage [L59]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.UploadFileToIManage [L12-L223]
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
      └─ uses_service HttpClient (SingleInstance)
        └─ method GetByteArrayAsync [L57]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 2
      └─ HttpClient
      └─ IManageApiClient
    └─ requests 1
      └─ UploadFileToIManageCommand
    └─ handlers 1
      └─ UploadFileToIManageCommandHandler

