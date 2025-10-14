[web] GET /api/super/storage/validate-document/{documentId:Guid}  (Dataverse.Api.Controllers.Super.StorageController.ValidateDocument)  [L57–L63] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request ValidateDocumentCommand [L60]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.ValidateDocumentCommandHandler.Handle [L26–L80]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L52]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method ReadQuery [L41]

