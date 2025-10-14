[web] POST /api/fyi/documents/upload-file  (DataGet.Api.Controllers.Integrations.FyiController.UploadFile)  [L165–L173] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request UploadFileCommand [L170]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.UploadFileCommandHandler.Handle [L23–L59]
      └─ uses_client HttpClient [L46]
      └─ uses_service HttpClient (SingleInstance)
        └─ method GetByteArrayAsync [L46]

