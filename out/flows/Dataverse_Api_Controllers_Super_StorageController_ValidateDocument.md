[web] GET /api/super/storage/validate-document/{documentId:Guid}  (Dataverse.Api.Controllers.Super.StorageController.ValidateDocument)  [L57–L63] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request ValidateDocumentCommand [L60]
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.ValidateDocumentCommandHandler.Handle [L26–L80]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L52]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method ReadQuery [L41]
          └─ ... (no implementation details available)

