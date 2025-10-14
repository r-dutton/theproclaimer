[web] GET /api/internal/storage/pending-operations  (Dataverse.Api.Controllers.Internal.StorageController.GetPendingDocumentVersionOperations)  [L209–L216] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetPendingDocumentVersionOperationsQuery [L213]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetPendingDocumentVersionOperationsQueryHandler.Handle [L26–L131]
      └─ calls TenantRepository.ReadTable [L53]

