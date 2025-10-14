[web] GET /api/ui/document-store-users/{userId:guid}/invite  (Dataverse.Api.Controllers.UI.Documents.DocumentStoreUserController.GetInvite)  [L35–L55] [auth=Authentication.UserPolicy]
  └─ calls DocumentStoreUserRepository.ReadQuery [L38]
  └─ queries DocumentStoreUser [L38]
    └─ reads_from DocumentStoreUsers
  └─ uses_service IControlledRepository<DocumentStoreUser>
    └─ method ReadQuery [L38]
  └─ sends_request CreateNewDocumentInviteCommand [L51]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.CreateNewDocumentInviteCommandHandler.Handle [L28–L70]
      └─ uses_service DocumentSecurityServiceFactory
        └─ method CreateInstance [L63]
      └─ uses_service IDocumentStoreService (AddScoped)
        └─ method GetReadOnlyDocumentStoresWithCredentials [L49]
  └─ sends_request CreateNewDocumentInviteCommand [L48]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.CreateNewDocumentInviteCommandHandler.Handle [L28–L70]
      └─ uses_service DocumentSecurityServiceFactory
        └─ method CreateInstance [L63]
      └─ uses_service IDocumentStoreService (AddScoped)
        └─ method GetReadOnlyDocumentStoresWithCredentials [L49]

