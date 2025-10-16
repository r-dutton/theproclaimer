[web] GET /api/internal/storage/pending-operations  (Dataverse.Api.Controllers.Internal.StorageController.GetPendingDocumentVersionOperations)  [L209–L216] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetPendingDocumentVersionOperationsQuery -> GetPendingDocumentVersionOperationsQueryHandler [L213]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetPendingDocumentVersionOperationsQueryHandler.Handle [L26–L131]
      └─ calls TenantRepository.ReadTable [L53]
  └─ impact_summary
    └─ requests 1
      └─ GetPendingDocumentVersionOperationsQuery
    └─ handlers 1
      └─ GetPendingDocumentVersionOperationsQueryHandler

