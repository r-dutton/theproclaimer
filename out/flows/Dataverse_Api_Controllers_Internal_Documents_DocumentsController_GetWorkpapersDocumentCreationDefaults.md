[web] GET /api/internal/documents/workpapers-creation-defaults  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.GetWorkpapersDocumentCreationDefaults)  [L159–L186] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls CabinetRepository.ReadQuery [L162]
  └─ calls DocumentStatusRepository.ReadQuery [L173]
  └─ queries Cabinet [L162]
    └─ reads_from Cabinets
  └─ queries DocumentStatus [L173]
    └─ reads_from DVS_DocumentStatuses
  └─ uses_service IControlledRepository<Cabinet>
    └─ method ReadQuery [L162]
  └─ uses_service IControlledRepository<DocumentStatus>
    └─ method ReadQuery [L173]

