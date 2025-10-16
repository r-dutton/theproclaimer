[web] PUT /api/internal/identity/request-email-change  (Dataverse.Api.Controllers.Internal.IdentityController.RequestEmailChange)  [L68–L93] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ maps_to CentralUserDto (var dto) [L86]
  └─ calls TenantRepository.ReadTable [L83]
  └─ query Tenant [L83]
    └─ reads_from Tenants
  └─ uses_service IMapper
    └─ method Map [L86]
      └─ ... (no implementation details available)
  └─ uses_service TenantRepository
    └─ method ReadTable [L83]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ sends_request UpdateCentralUser [L90]
    └─ handled_by Cirrus.Central.Commands.UpdateCentralUserHandler.Handle [L37–L93]
      └─ calls CentralRepository.Commit [L77]
      └─ calls CentralRepository.WriteTable [L66]
      └─ calls CentralRepository.ReadTable [L53]
      └─ uses_service DataverseService
        └─ method GetStandardInviteInfo [L86]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.GetStandardInviteInfo [L14-L66]

