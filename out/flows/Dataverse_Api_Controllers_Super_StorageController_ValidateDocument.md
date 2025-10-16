[web] GET /api/super/storage/validate-document/{documentId:Guid}  (Dataverse.Api.Controllers.Super.StorageController.ValidateDocument)  [L57–L63] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request ValidateDocumentCommand -> ValidateDocumentCommandHandler [L60]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.ValidateDocumentCommandHandler.Handle [L26–L80]
      └─ calls DocumentVersionRepository.ReadQuery [L41]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L52]
          └─ implementation DocumentServiceFactory.GetDefaultColdStorage
  └─ impact_summary
    └─ requests 1
      └─ ValidateDocumentCommand
    └─ handlers 1
      └─ ValidateDocumentCommandHandler

