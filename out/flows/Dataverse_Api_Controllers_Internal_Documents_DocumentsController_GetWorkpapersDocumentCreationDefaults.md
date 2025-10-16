[web] GET /api/internal/documents/workpapers-creation-defaults  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.GetWorkpapersDocumentCreationDefaults)  [L159–L186] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls CabinetRepository.ReadQuery [L162]
  └─ calls DocumentStatusRepository.ReadQuery [L173]
  └─ query Cabinet [L162]
    └─ reads_from Cabinets
  └─ query DocumentStatus [L173]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<Cabinet>
    └─ method ReadQuery [L162]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method ReadQuery [L173]
      └─ ... (no implementation details available)

