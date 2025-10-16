[web] POST /api/internal/documents/create-document-m2m  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateDocumentViaM2M)  [L227–L236] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service StorageService
    └─ method MoveFile [L232]
      └─ implementation Dataverse.Services.Features.Storage.StorageService.MoveFile [L18-L331]
  └─ uses_storage StorageService.MoveFile [L232]
  └─ sends_request CreateDocumentCommand [L234]
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.CreateDocumentCommandHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method CreateDocument [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.CreateDocument [L30-L166]
  └─ returns FyiDocumentDto [L234]

