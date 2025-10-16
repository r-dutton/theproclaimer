[web] GET /api/internal/accounting/files/exist-for-client/{dataverseClientId:guid}  (Cirrus.Api.Controllers.Internal.FilesController.CheckForClientFiles)  [L38–L47] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls FileRepository.ReadQuery [L41]
  └─ query File [L41]
    └─ reads_from Files
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ File writes=0 reads=1

