[web] GET /api/internal/documents/workpapers-creation-defaults  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.GetWorkpapersDocumentCreationDefaults)  [L159–L186] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DocumentStatusRepository.ReadQuery [L173]
  └─ calls CabinetRepository.ReadQuery [L162]
  └─ query DocumentStatus [L173]
    └─ reads_from DVS_DocumentStatuses
  └─ query Cabinet [L162]
    └─ reads_from Cabinets
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ Cabinet writes=0 reads=1
      └─ DocumentStatus writes=0 reads=1

