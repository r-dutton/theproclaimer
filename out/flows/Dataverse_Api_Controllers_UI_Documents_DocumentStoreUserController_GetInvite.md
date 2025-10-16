[web] GET /api/ui/document-store-users/{userId:guid}/invite  (Dataverse.Api.Controllers.UI.Documents.DocumentStoreUserController.GetInvite)  [L35–L55] status=200 [auth=Authentication.UserPolicy]
  └─ calls DocumentStoreUserRepository.ReadQuery [L38]
  └─ query DocumentStoreUser [L38]
    └─ reads_from DocumentStoreUsers
  └─ uses_service IControlledRepository<DocumentStoreUser>
    └─ method ReadQuery [L38]
      └─ ... (no implementation details available)
  └─ sends_request CreateNewDocumentInviteCommand [L51]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.CreateNewDocumentInviteCommandHandler.Handle [L28–L70]
      └─ uses_service DocumentSecurityServiceFactory
        └─ method CreateInstance [L63]
          └─ ... (no implementation details available)
      └─ uses_service IDocumentStoreService (AddScoped)
        └─ method GetReadOnlyDocumentStoresWithCredentials [L49]
          └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
      └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L49]
  └─ sends_request CreateNewDocumentInviteCommand [L48]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.CreateNewDocumentInviteCommandHandler.Handle [L28–L70]
      └─ uses_service DocumentSecurityServiceFactory
        └─ method CreateInstance [L63]
          └─ ... (no implementation details available)
      └─ uses_service IDocumentStoreService (AddScoped)
        └─ method GetReadOnlyDocumentStoresWithCredentials [L49]
          └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
      └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L49]

