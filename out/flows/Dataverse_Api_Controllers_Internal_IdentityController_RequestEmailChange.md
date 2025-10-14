[web] PUT /api/internal/identity/request-email-change  (Dataverse.Api.Controllers.Internal.IdentityController.RequestEmailChange)  [L68–L93] [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to CentralUserDto (var dto) [L86]
  └─ calls TenantRepository.ReadTable [L83]
  └─ queries Tenant [L83]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L86]
  └─ uses_service TenantRepository
    └─ method ReadTable [L83]
  └─ sends_request UpdateCentralUser [L90]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Commands.UpdateCentralUserHandler.Handle [L37–L93]
      └─ calls CentralRepository.Commit [L77]
      └─ calls CentralRepository.WriteTable [L66]
      └─ calls CentralRepository.ReadTable [L53]
      └─ uses_service DataverseService
        └─ method GetStandardInviteInfo [L86]

