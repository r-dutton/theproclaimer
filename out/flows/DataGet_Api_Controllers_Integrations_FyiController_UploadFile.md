[web] POST /api/fyi/documents/upload-file  (DataGet.Api.Controllers.Integrations.FyiController.UploadFile)  [L165–L173] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request UploadFileCommand -> UploadFileCommandHandler [L170]
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.UploadFileCommandHandler.Handle [L23–L59]
      └─ uses_client HttpClient [L46]
      └─ uses_service HttpClient (SingleInstance)
        └─ method GetByteArrayAsync [L46]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ HttpClient
    └─ requests 1
      └─ UploadFileCommand
    └─ handlers 1
      └─ UploadFileCommandHandler

