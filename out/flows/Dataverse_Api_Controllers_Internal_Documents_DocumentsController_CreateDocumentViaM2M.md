[web] POST /api/internal/documents/create-document-m2m  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateDocumentViaM2M)  [L227–L236] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service StorageService
    └─ method MoveFile [L232]
  └─ sends_request CreateDocumentCommand [L234]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.CreateDocumentCommandHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method CreateDocument [L29]
  └─ returns FyiDocumentDto [L234]

