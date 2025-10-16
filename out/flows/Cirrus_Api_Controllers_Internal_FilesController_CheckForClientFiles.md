[web] GET /api/internal/accounting/files/exist-for-client/{dataverseClientId:guid}  (Cirrus.Api.Controllers.Internal.FilesController.CheckForClientFiles)  [L38–L47] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls FileRepository.ReadQuery [L41]
  └─ query File [L41]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L41]
      └─ ... (no implementation details available)

