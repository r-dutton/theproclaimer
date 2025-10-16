[web] POST /api/internal/documents/{documentId:guid}/versions/add-document-version-M2M  (Dataverse.Api.Controllers.Internal.Documents.DocumentVersionsController.AddDocumentVersionViaM2M)  [L36–L45] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service StorageService
    └─ method MoveFile [L42]
      └─ implementation Dataverse.Services.Features.Storage.StorageService.MoveFile [L18-L331]
  └─ uses_storage StorageService.MoveFile [L42]

