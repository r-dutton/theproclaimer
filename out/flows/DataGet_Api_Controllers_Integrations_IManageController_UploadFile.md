[web] POST /api/imanage/customers/{customerId:int}/libraries/{library}/client-selector/{clientSelector}/documents  (DataGet.Api.Controllers.Integrations.IManageController.UploadFile)  [L258–L268] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request UploadFileToIManageCommand [L266]
    └─ handled_by DataGet.Integrations.iManage.Api.Command.UploadFileToIManageCommandHandler.Handle [L27–L83]
      └─ uses_client HttpClient [L57]
      └─ uses_service IManageService
        └─ method UploadFileToIManage [L59]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.UploadFileToIManage [L12-L223]
      └─ uses_service HttpClient (SingleInstance)
        └─ method GetByteArrayAsync [L57]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<IntegrationConfiguration>
        └─ method ReadQuery [L66]
          └─ ... (no implementation details available)

