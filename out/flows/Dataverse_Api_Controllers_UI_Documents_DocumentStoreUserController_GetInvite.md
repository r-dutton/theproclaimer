[web] GET /api/ui/document-store-users/{userId:guid}/invite  (Dataverse.Api.Controllers.UI.Documents.DocumentStoreUserController.GetInvite)  [L35–L55] status=200 [auth=Authentication.UserPolicy]
  └─ calls DocumentStoreUserRepository.ReadQuery [L38]
  └─ query DocumentStoreUser [L38]
    └─ reads_from DocumentStoreUsers
  └─ sends_request CreateNewDocumentInviteCommand -> CreateNewDocumentInviteCommandHandler [L51]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.CreateNewDocumentInviteCommandHandler.Handle [L28–L70]
      └─ uses_service DocumentSecurityServiceFactory
        └─ method CreateInstance [L63]
          └─ ... (no implementation details available)
      └─ uses_service IDocumentStoreService (AddScoped)
        └─ method GetReadOnlyDocumentStoresWithCredentials [L49]
          └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
            └─ uses_service IControlledRepository<DocumentStore> (Scoped (inferred))
              └─ method GetReadOnlyDocumentStoresWithCredentials [L66]
                └─ implementation Dataverse.Data.Repository.Documents.DocumentStoreRepository.GetReadOnlyDocumentStoresWithCredentials
            └─ uses_service TenantService
              └─ method GetReadOnlyDocumentStoresWithCredentials [L57]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetReadOnlyDocumentStoresWithCredentials [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
            └─ maps_to SecureDocumentStoreDto [L66]
              └─ automapper.registration DataverseMappingProfile (DocumentStore->SecureDocumentStoreDto) [L438]
            └─ uses_cache IDistributedCache.SetRecordAsync [write] [L71]
            └─ uses_cache IDistributedCache.GetRecordAsync [read] [L62]
      └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L49]
  └─ sends_request CreateNewDocumentInviteCommand -> CreateNewDocumentInviteCommandHandler [L48]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DocumentStoreUser writes=0 reads=1
    └─ requests 1
      └─ CreateNewDocumentInviteCommand
    └─ handlers 1
      └─ CreateNewDocumentInviteCommandHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ SecureDocumentStoreDto

