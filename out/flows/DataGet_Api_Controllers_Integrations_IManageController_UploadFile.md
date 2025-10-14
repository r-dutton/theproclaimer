[web] POST /api/imanage/customers/{customerId:int}/libraries/{library}/client-selector/{clientSelector}/documents  (DataGet.Api.Controllers.Integrations.IManageController.UploadFile)  [L258–L268] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request UploadFileToIManageCommand [L266]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Command.UploadFileToIManageCommandHandler.Handle [L27–L83]
      └─ uses_client HttpClient [L57]
      └─ uses_service HttpClient (SingleInstance)
        └─ method GetByteArrayAsync [L57]
      └─ uses_service IControlledRepository<IntegrationConfiguration>
        └─ method ReadQuery [L66]
      └─ uses_service IManageService
        └─ method UploadFileToIManage [L59]

